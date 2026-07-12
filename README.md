# Advanced Helicopter Physics & Camera System

![Unity Version](https://img.shields.io/badge/Unity-6.3%20LTS-blue)
![C# Version](https://img.shields.io/badge/C%23-10.0-green)
![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20VR-orange)

A component-based flight simulation framework for Unity, featuring a physically accurate helicopter controller, decoupled input abstraction, and a dynamic camera management system. 

---

## 🚀 Key Features

* **Advanced Aerodynamics & Rotor Physics:** Simulates core flight mechanics including main rotor lift, cyclic/collective pitch, torque generation, tail rotor compensation, drag forces, and weight distribution.
* **Smart Camera Management System:** Smooth real-time switching between specialized camera behaviors—such as direct cockpit view (`IP_Cockpit_HeliCamera`), advanced tracking, and modular base setups—all managed by a central supervisor.
* **Custom Unity Editor Extensions:** Includes bespoke inspector scripts (`IP_AdvancedHeliCamera_Editor`) to streamline the calibration of camera tracking parameters directly inside the Unity Editor scene view.
* **Decoupled Input System:** Features an extensible input architecture with out-of-the-box support for Keyboard, Xbox Gamepads, and Mobile layouts, built on top of a clean interface abstraction (`IP_IHeliCamera`, `IP_BaseHeli_Input`).
* **Visual Rotor Effects:** Implements script-driven, performance-friendly alpha/texture blending (`IP_Rotor_Blur`) that dynamically reacts to the engine's current RPM and rotor velocity.

---

## 📺 Flight Demo

![Flight Simulation Demo](path_to_your_compressed_video_or_gif.gif)
*Demonstration of real-time torque compensation, aerodynamics testing, and seamless camera state shifting.*

---

## 📁 Project Architecture

The core architecture is strictly decoupled into modular subsystems:

```text
Scripts/
├── Camera/           # Core camera logic (Base, Cockpit, Advanced) and Custom Inspectors
├── Characteristics/  # Heli aerodynamics, performance indices, and physical profiles
├── Controllers/      # Main flight controllers, system supervisors, and rotor managers
├── Engines/          # Powerplants, engine mechanics, and RPM/throttle systems
├── Old Input/        # Legacy input mapping (Keyboard, Xbox controller, and Mobile schemes)
├── Rigidbodies/      # Custom Rigidbody abstractions and physics body controllers
├── Rotors/           # Rotor interfaces and dynamic motion-blur visual effects
└── Testing/          # Isolated sandbox components for calculating Forces, Hover, Weight, and Torque

📝 Roadmap
[x] Implement robust Hover, Weight, and Torque physical tests.
[x] Build custom inspector layout for advanced camera tracking setup.
[ ] Migrate the legacy input pipeline (Old Input) to the new Unity Input System package.
[ ] Implement fully integrated VR interactions (XR Interaction Toolkit) for physical cockpit controls.
