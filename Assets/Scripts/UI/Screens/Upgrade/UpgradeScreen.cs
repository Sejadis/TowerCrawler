using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using SejDev.Systems.UI;
using UnityEngine;

namespace SejDev.UI
{
    public class UpgradeScreen : UIScreen
    {
        [SerializeField] private GameObject upgradeHolderPrefab;
        [SerializeField] private GameObject groupPrefab;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private GameObject vertParent;
        [SerializeField] private ObjectDescriber describer;

        private UpgradeTree upgradeTree;
        private readonly List<UpgradeElement> upgradeElements = new List<UpgradeElement>();
        private readonly Dictionary<int, GameObject> conditionGroups = new Dictionary<int, GameObject>();

        public void CreateFromUpgradeTree(UpgradeTree tree)
        {
            CleanUpTree();
            upgradeTree = tree;
            if (upgradeTree == null) return;

            foreach (var upgradeRelation in upgradeTree.upgrades)
            {
                if (!conditionGroups.ContainsKey(upgradeRelation.requiredPointsSpent))
                {
                    conditionGroups[upgradeRelation.requiredPointsSpent] =
                        Instantiate(groupPrefab, vertParent.transform);
                    ReorderGroups();
                }

                var elementObj = Instantiate(upgradeHolderPrefab,
                    conditionGroups[upgradeRelation.requiredPointsSpent].transform);
                // holderObj.name += upgradeRelation.upgrade.name;
                var element = elementObj.GetComponent<UpgradeElement>();

                element?.Bind(upgradeRelation.upgrade, this);
                element.OnElementEnter += OnUpgradeElementEnter;
                element.OnElementExit += OnUpgradeElementExit;
                upgradeElements.Add(element);
            }

            StartCoroutine(DrawLines());
        }

        private void OnUpgradeElementExit(object sender, AbilityUpgrade e)
        {
            describer.Reset();
        }

        private void OnUpgradeElementEnter(object sender, AbilityUpgrade e)
        {
            describer.Fill(e);
        }

        private void ReorderGroups()
        {
            var sortedKeys = conditionGroups.Keys.OrderBy(key => key).ToArray();
            for (int i = 0; i < sortedKeys.Length; i++)
            {
                conditionGroups[sortedKeys[i]].transform.SetSiblingIndex(i);
            }
        }

        private IEnumerator DrawLines()
        {
            yield return null;
            foreach (var upgradeRelation in upgradeTree.upgrades)
            {
                var obj = upgradeElements.First(element => element.Upgrade.Equals(upgradeRelation.upgrade)).gameObject;
                var inTransform = obj.GetComponent<UpgradeElement>().IN;
                foreach (var requiredUpgrade in upgradeRelation.requiredUpgrades)
                {
                    var reqObj = upgradeElements.First(element => element.Upgrade.Equals(requiredUpgrade)).gameObject;
                    var outTransform = reqObj.GetComponent<UpgradeElement>().OUT;

                    var lineObj = CreateLineObject(inTransform);
                    var lineRenderer = lineObj.AddComponent<LineRenderer>();
                    InitializeLineRenderer(lineRenderer);
                    lineRenderer.SetPositions(GetPositions(inTransform, outTransform));
                }
            }
        }

        private GameObject CreateLineObject(Transform parent)
        {
            var lineObj = new GameObject("LineObject");
            lineObj.layer = LayerMask.NameToLayer("UI");
            lineObj.transform.SetParent(parent);
            lineObj.transform.localPosition = Vector3.zero;
            lineObj.transform.localRotation = Quaternion.identity;
            lineObj.transform.localScale = Vector3.one;
            return lineObj;
        }

        private void InitializeLineRenderer(LineRenderer renderer)
        {
            renderer.startColor = Color.red;
            renderer.endColor = Color.red;
            renderer.material = lineMaterial;
            renderer.startWidth = 2f;
            renderer.endWidth = 2f;
            renderer.useWorldSpace = false;
        }

        private Vector3[] GetPositions(Transform obj1, Transform obj2)
        {
            Vector3[] array =
            {
                Vector3.zero,
                obj1.InverseTransformPoint(obj2.position)
            };
            return array;
        }

        private void CleanUpTree()
        {
            upgradeElements.Clear();
            foreach (var key in conditionGroups.Keys)
            {
                foreach (Transform child in conditionGroups[key].transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public void ChangeState(string id)
        {
            upgradeTree.ChangeState(id, out var implicitChanges);
            foreach (var implicitChange in implicitChanges)
            {
                upgradeElements.First(holder => holder.Upgrade.GUID.Equals(implicitChange)).UpdateState();
            }
        }
    }
}