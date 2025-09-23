# 🎮 OOP Tetris (Console, C#)

A console-based Tetris clone built in C# to practice Object-Oriented Programming (OOP) principles.

---

 Features
- Encapsulation → each class manages its own logic  
- Abstraction → methods hide internal details (board grid, rotations)  
- Inheritance → `Tetromino` inherits from `Character`  
- Polymorphism → `PrintInfo` is overridden in `Tetromino`  
- Single Responsibility Principle (SRP) → one class = one job  

---

  Controls
- ← / → → Move left/right  
- ↓ → Soft drop  
- ↑ / X → Rotate clockwise  
- Z → Rotate counter-clockwise  
- Space → Hard drop  
- Q → Quit  

---

 How to Run
```Terminal
dotnet build
dotnet run
