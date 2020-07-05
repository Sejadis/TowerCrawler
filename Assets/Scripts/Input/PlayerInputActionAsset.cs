// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActionAsset : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActionAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""4e2e675d-5a4b-4b1b-884f-6745fdb8454c"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5743dc99-5929-40c1-b9ca-dc12079847be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""6d60ef67-b0f4-42c9-9591-46df6b1bfc04"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponBase"",
                    ""type"": ""Button"",
                    ""id"": ""215c5b08-8e13-40d0-942e-9b8055a3696e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponSpecial"",
                    ""type"": ""Button"",
                    ""id"": ""23d157c5-9397-44b8-9897-b76e4a5a5d70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core1"",
                    ""type"": ""Button"",
                    ""id"": ""1832f652-9b35-425d-bf72-23dcf459059f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core2"",
                    ""type"": ""Button"",
                    ""id"": ""0dfd9319-a4bc-41dc-a550-e67a7b43b5d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core3"",
                    ""type"": ""Button"",
                    ""id"": ""e1db2890-6997-4ce4-b139-ca77bf9e4971"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1f0216bc-b80f-46cd-8677-5ff6eeb3f13c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f6bc8f3c-8a51-4172-a4b1-6f8f7fc38e3a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7ca2b5ea-ad2a-4b83-833a-a0b71960065b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c0c7c20f-e6f2-4cbf-86c1-8dac51fa128d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f56b13f6-fddf-46aa-8f93-e2bb929ac4d2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4bd6669e-2999-4163-a425-5972764bcb0c"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4575ecaa-a973-461c-ae39-605d2d9f3f3e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a170a71-97fa-4b62-bcd3-ef3719ee8f3c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponBase"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d2f8b94-08a9-4a35-a917-ddba905e4fc0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponSpecial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf9a54b-d2b3-4f21-937b-8d3460fdb638"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d86b0b04-7658-4f05-87d4-2263025033d6"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Abilities"",
            ""id"": ""9884e74a-287a-4181-b1a6-d500404175cf"",
            ""actions"": [
                {
                    ""name"": ""WeaponBase"",
                    ""type"": ""Button"",
                    ""id"": ""8f9019fa-8eb6-4b21-b2da-cc2130fd4db1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponSpecial"",
                    ""type"": ""Button"",
                    ""id"": ""d5618a31-2c8e-4a8a-a165-fe782bb9cd7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core1"",
                    ""type"": ""Button"",
                    ""id"": ""6e891ba9-1807-49b0-bb91-b1b0a5f13a56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core2"",
                    ""type"": ""Button"",
                    ""id"": ""67e96f05-9a1d-4735-9e36-3166ea010734"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core3"",
                    ""type"": ""Button"",
                    ""id"": ""08d55979-7a7f-4888-8d1c-eb07b892c313"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1feb268d-ad29-464d-ba33-88b020c333cf"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponBase"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5100e3f7-04bb-4fb4-9a8b-ec6fe173080e"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponSpecial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a061e13-194b-4584-8638-64a467f332fd"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba91d9da-86b5-4253-b859-6076e4d285cb"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4958ec5-1844-41f0-84f1-b55878a2d84c"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Core3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Look = m_Controls.FindAction("Look", throwIfNotFound: true);
        m_Controls_WeaponBase = m_Controls.FindAction("WeaponBase", throwIfNotFound: true);
        m_Controls_WeaponSpecial = m_Controls.FindAction("WeaponSpecial", throwIfNotFound: true);
        m_Controls_Core1 = m_Controls.FindAction("Core1", throwIfNotFound: true);
        m_Controls_Core2 = m_Controls.FindAction("Core2", throwIfNotFound: true);
        m_Controls_Core3 = m_Controls.FindAction("Core3", throwIfNotFound: true);
        // Abilities
        m_Abilities = asset.FindActionMap("Abilities", throwIfNotFound: true);
        m_Abilities_WeaponBase = m_Abilities.FindAction("WeaponBase", throwIfNotFound: true);
        m_Abilities_WeaponSpecial = m_Abilities.FindAction("WeaponSpecial", throwIfNotFound: true);
        m_Abilities_Core1 = m_Abilities.FindAction("Core1", throwIfNotFound: true);
        m_Abilities_Core2 = m_Abilities.FindAction("Core2", throwIfNotFound: true);
        m_Abilities_Core3 = m_Abilities.FindAction("Core3", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Look;
    private readonly InputAction m_Controls_WeaponBase;
    private readonly InputAction m_Controls_WeaponSpecial;
    private readonly InputAction m_Controls_Core1;
    private readonly InputAction m_Controls_Core2;
    private readonly InputAction m_Controls_Core3;
    public struct ControlsActions
    {
        private @PlayerInputActionAsset m_Wrapper;
        public ControlsActions(@PlayerInputActionAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Look => m_Wrapper.m_Controls_Look;
        public InputAction @WeaponBase => m_Wrapper.m_Controls_WeaponBase;
        public InputAction @WeaponSpecial => m_Wrapper.m_Controls_WeaponSpecial;
        public InputAction @Core1 => m_Wrapper.m_Controls_Core1;
        public InputAction @Core2 => m_Wrapper.m_Controls_Core2;
        public InputAction @Core3 => m_Wrapper.m_Controls_Core3;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @WeaponBase.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponBase;
                @WeaponSpecial.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnWeaponSpecial;
                @Core1.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore1;
                @Core1.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore1;
                @Core1.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore1;
                @Core2.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore2;
                @Core2.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore2;
                @Core2.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore2;
                @Core3.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore3;
                @Core3.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore3;
                @Core3.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCore3;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @WeaponBase.started += instance.OnWeaponBase;
                @WeaponBase.performed += instance.OnWeaponBase;
                @WeaponBase.canceled += instance.OnWeaponBase;
                @WeaponSpecial.started += instance.OnWeaponSpecial;
                @WeaponSpecial.performed += instance.OnWeaponSpecial;
                @WeaponSpecial.canceled += instance.OnWeaponSpecial;
                @Core1.started += instance.OnCore1;
                @Core1.performed += instance.OnCore1;
                @Core1.canceled += instance.OnCore1;
                @Core2.started += instance.OnCore2;
                @Core2.performed += instance.OnCore2;
                @Core2.canceled += instance.OnCore2;
                @Core3.started += instance.OnCore3;
                @Core3.performed += instance.OnCore3;
                @Core3.canceled += instance.OnCore3;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);

    // Abilities
    private readonly InputActionMap m_Abilities;
    private IAbilitiesActions m_AbilitiesActionsCallbackInterface;
    private readonly InputAction m_Abilities_WeaponBase;
    private readonly InputAction m_Abilities_WeaponSpecial;
    private readonly InputAction m_Abilities_Core1;
    private readonly InputAction m_Abilities_Core2;
    private readonly InputAction m_Abilities_Core3;
    public struct AbilitiesActions
    {
        private @PlayerInputActionAsset m_Wrapper;
        public AbilitiesActions(@PlayerInputActionAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @WeaponBase => m_Wrapper.m_Abilities_WeaponBase;
        public InputAction @WeaponSpecial => m_Wrapper.m_Abilities_WeaponSpecial;
        public InputAction @Core1 => m_Wrapper.m_Abilities_Core1;
        public InputAction @Core2 => m_Wrapper.m_Abilities_Core2;
        public InputAction @Core3 => m_Wrapper.m_Abilities_Core3;
        public InputActionMap Get() { return m_Wrapper.m_Abilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilitiesActions set) { return set.Get(); }
        public void SetCallbacks(IAbilitiesActions instance)
        {
            if (m_Wrapper.m_AbilitiesActionsCallbackInterface != null)
            {
                @WeaponBase.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponSpecial.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @Core1.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core1.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core1.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core2.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core2.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core2.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core3.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
                @Core3.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
                @Core3.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
            }
            m_Wrapper.m_AbilitiesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WeaponBase.started += instance.OnWeaponBase;
                @WeaponBase.performed += instance.OnWeaponBase;
                @WeaponBase.canceled += instance.OnWeaponBase;
                @WeaponSpecial.started += instance.OnWeaponSpecial;
                @WeaponSpecial.performed += instance.OnWeaponSpecial;
                @WeaponSpecial.canceled += instance.OnWeaponSpecial;
                @Core1.started += instance.OnCore1;
                @Core1.performed += instance.OnCore1;
                @Core1.canceled += instance.OnCore1;
                @Core2.started += instance.OnCore2;
                @Core2.performed += instance.OnCore2;
                @Core2.canceled += instance.OnCore2;
                @Core3.started += instance.OnCore3;
                @Core3.performed += instance.OnCore3;
                @Core3.canceled += instance.OnCore3;
            }
        }
    }
    public AbilitiesActions @Abilities => new AbilitiesActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnWeaponBase(InputAction.CallbackContext context);
        void OnWeaponSpecial(InputAction.CallbackContext context);
        void OnCore1(InputAction.CallbackContext context);
        void OnCore2(InputAction.CallbackContext context);
        void OnCore3(InputAction.CallbackContext context);
    }
    public interface IAbilitiesActions
    {
        void OnWeaponBase(InputAction.CallbackContext context);
        void OnWeaponSpecial(InputAction.CallbackContext context);
        void OnCore1(InputAction.CallbackContext context);
        void OnCore2(InputAction.CallbackContext context);
        void OnCore3(InputAction.CallbackContext context);
    }
}
