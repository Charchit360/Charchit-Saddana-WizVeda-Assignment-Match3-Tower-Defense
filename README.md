# Charchit-Saddana-WizVeda-Assignment-Match3-Tower-Defense
### Match-3 + Tower Defense (Unity Mobile Game)

This project was created as part of a **Unity Developer Technical Round**.  
It combines classic **Match-3 puzzle mechanics** with a **Tower Defense system**, designed for mobile play.

---

## 🎮 Controls
- **Match-3:**  
  - Tap two adjacent tiles to swap them.  
  - If they form a valid match (3+), tiles are cleared and coins are rewarded.  
  - If no match is formed, tiles return to their original positions.  

- **Tower Placement:**  
  - Tap on predefined grey slots along the enemy path to place a tower (costs coins).  

- **Tower Upgrade:**  
  - Match **4+ tiles** to unlock an upgrade.  
  - Tap on an existing tower to upgrade its **range** or **fire rate**.  

- **Defense:**  
  - Enemies reduce **Base HP** if they reach the end of the path.  

---

## ✨ Features Implemented

### **Round 1 (Core Requirements)**
- **Puzzle System**  
  - 6×6 grid, multiple tile types.  
  - Match-3 clearing generates coins.  

- **Tower Defense**  
  - Predefined tower slots along a single enemy path.  
  - Basic tower auto-shoots enemies in range.  
  - Enemy spawns with health.  

- **Resource System**  
  - Towers cost coins (earned via matches).  

- **Game Loop**  
  - HUD showing Coins, Base HP, and Wave Counter.  
  - **Victory:** Survive all waves.  
  - **Defeat:** Base HP reaches 0.  

---

### **Round 2 (Extended Features)**
- **Tower Placement with Contextual Menu**  
  - Tapping a slot shows available tower options with coin costs.  

- **Tower Upgrade via Match-3 Mega Matches**  
  - Match of 4+ tiles allows tower upgrade.  
  - Player selects which tower to enhance.  

- **Wave Progression**  
  - At least 3 waves with increasing difficulty.  
  - UI feedback for wave number and player health.  

- **Victory / Defeat Conditions**  
  - Clear all waves → **Victory**.  
  - Base HP = 0 → **Defeat**.  

---

## 📱 Mobile Readiness
- Touch-based controls (tap & swap).  
- UI scaled for mobile screens.  

---

## 🛠️ Tech & Notes
- Built with **Unity** (6 LTS).  
- Designed and implemented within a **2-day time frame**.  
- Scope limited to 1 tower and 1 enemy type (per test requirements).  
- Extended features added for **Round 2** to demonstrate system integration.  
