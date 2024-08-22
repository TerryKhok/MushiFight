//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerControls02.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls02: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls02()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls02"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""20543023-0cee-49e5-b111-fe46a2c46914"",
            ""actions"": [
                {
                    ""name"": ""p2Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7270afc8-c9a9-4726-9af1-ddbee4062830"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""p2Skill"",
                    ""type"": ""Button"",
                    ""id"": ""a4761551-1e18-4b87-b218-9065e5eab14d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""p2HeadMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""906e6b80-0605-4186-89e5-a26706731980"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1eeb6c41-ff15-448a-b452-003eee45b72a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""934be5ec-2322-470f-a41c-84c790c4c9ad"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""69bb02ad-88b0-45fd-9106-ed00faf4a048"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b1850c8-d685-4965-a3f7-fc39d1623f44"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2223b056-a516-45be-bdd8-9a129edfd534"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""436eafc9-ed94-4602-a986-afb8f9530bcf"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d31c8b77-74da-4dbf-b90a-cf9d7af5a92d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2HeadMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3e688c49-4a5c-4fe7-8e7f-26fa90837ef3"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2HeadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9246ce1b-2db2-4f71-84d3-291e78e1ce41"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2HeadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""99b2aa53-e7fc-43d3-8fe2-cf684cff7a50"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2HeadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""59f66cbf-e8ac-4c55-a169-c97533c15ca1"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2HeadMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Select"",
            ""id"": ""1a7e9ae5-0f4d-42be-ab80-8d3cc67d58c7"",
            ""actions"": [
                {
                    ""name"": ""p2Decide"",
                    ""type"": ""Button"",
                    ""id"": ""0872966d-dba8-4475-8d9f-4cb198ae541d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""p2CursorMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2b49a99b-100a-42a0-91aa-ce943cb29c61"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""80b7042f-1a45-4a0a-a3a8-486cd947b24c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2Decide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b22eec95-9f2b-4110-8ce8-3b7dbecce1d3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""p2CursorMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e2e2c28e-a989-4417-a3ff-09cb8ea681a7"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aea80644-7f4d-4dbc-829b-df30dd840596"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6aa37750-511e-47b3-a0f6-738d3a740ac2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""96cf3fb1-20b0-48e3-aa0f-a6506c889474"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""p2CursorMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_p2Movement = m_Gameplay.FindAction("p2Movement", throwIfNotFound: true);
        m_Gameplay_p2Skill = m_Gameplay.FindAction("p2Skill", throwIfNotFound: true);
        m_Gameplay_p2HeadMovement = m_Gameplay.FindAction("p2HeadMovement", throwIfNotFound: true);
        // Select
        m_Select = asset.FindActionMap("Select", throwIfNotFound: true);
        m_Select_p2Decide = m_Select.FindAction("p2Decide", throwIfNotFound: true);
        m_Select_p2CursorMove = m_Select.FindAction("p2CursorMove", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_p2Movement;
    private readonly InputAction m_Gameplay_p2Skill;
    private readonly InputAction m_Gameplay_p2HeadMovement;
    public struct GameplayActions
    {
        private @PlayerControls02 m_Wrapper;
        public GameplayActions(@PlayerControls02 wrapper) { m_Wrapper = wrapper; }
        public InputAction @p2Movement => m_Wrapper.m_Gameplay_p2Movement;
        public InputAction @p2Skill => m_Wrapper.m_Gameplay_p2Skill;
        public InputAction @p2HeadMovement => m_Wrapper.m_Gameplay_p2HeadMovement;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @p2Movement.started += instance.OnP2Movement;
            @p2Movement.performed += instance.OnP2Movement;
            @p2Movement.canceled += instance.OnP2Movement;
            @p2Skill.started += instance.OnP2Skill;
            @p2Skill.performed += instance.OnP2Skill;
            @p2Skill.canceled += instance.OnP2Skill;
            @p2HeadMovement.started += instance.OnP2HeadMovement;
            @p2HeadMovement.performed += instance.OnP2HeadMovement;
            @p2HeadMovement.canceled += instance.OnP2HeadMovement;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @p2Movement.started -= instance.OnP2Movement;
            @p2Movement.performed -= instance.OnP2Movement;
            @p2Movement.canceled -= instance.OnP2Movement;
            @p2Skill.started -= instance.OnP2Skill;
            @p2Skill.performed -= instance.OnP2Skill;
            @p2Skill.canceled -= instance.OnP2Skill;
            @p2HeadMovement.started -= instance.OnP2HeadMovement;
            @p2HeadMovement.performed -= instance.OnP2HeadMovement;
            @p2HeadMovement.canceled -= instance.OnP2HeadMovement;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Select
    private readonly InputActionMap m_Select;
    private List<ISelectActions> m_SelectActionsCallbackInterfaces = new List<ISelectActions>();
    private readonly InputAction m_Select_p2Decide;
    private readonly InputAction m_Select_p2CursorMove;
    public struct SelectActions
    {
        private @PlayerControls02 m_Wrapper;
        public SelectActions(@PlayerControls02 wrapper) { m_Wrapper = wrapper; }
        public InputAction @p2Decide => m_Wrapper.m_Select_p2Decide;
        public InputAction @p2CursorMove => m_Wrapper.m_Select_p2CursorMove;
        public InputActionMap Get() { return m_Wrapper.m_Select; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectActions set) { return set.Get(); }
        public void AddCallbacks(ISelectActions instance)
        {
            if (instance == null || m_Wrapper.m_SelectActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SelectActionsCallbackInterfaces.Add(instance);
            @p2Decide.started += instance.OnP2Decide;
            @p2Decide.performed += instance.OnP2Decide;
            @p2Decide.canceled += instance.OnP2Decide;
            @p2CursorMove.started += instance.OnP2CursorMove;
            @p2CursorMove.performed += instance.OnP2CursorMove;
            @p2CursorMove.canceled += instance.OnP2CursorMove;
        }

        private void UnregisterCallbacks(ISelectActions instance)
        {
            @p2Decide.started -= instance.OnP2Decide;
            @p2Decide.performed -= instance.OnP2Decide;
            @p2Decide.canceled -= instance.OnP2Decide;
            @p2CursorMove.started -= instance.OnP2CursorMove;
            @p2CursorMove.performed -= instance.OnP2CursorMove;
            @p2CursorMove.canceled -= instance.OnP2CursorMove;
        }

        public void RemoveCallbacks(ISelectActions instance)
        {
            if (m_Wrapper.m_SelectActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISelectActions instance)
        {
            foreach (var item in m_Wrapper.m_SelectActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SelectActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SelectActions @Select => new SelectActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnP2Movement(InputAction.CallbackContext context);
        void OnP2Skill(InputAction.CallbackContext context);
        void OnP2HeadMovement(InputAction.CallbackContext context);
    }
    public interface ISelectActions
    {
        void OnP2Decide(InputAction.CallbackContext context);
        void OnP2CursorMove(InputAction.CallbackContext context);
    }
}