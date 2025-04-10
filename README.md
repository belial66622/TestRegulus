# TestRegulus
 
Dalam game ini ada beberapa script yang saya gunakan:
1.input -> InputControl.cs
Input yang saya gunakan adalah newinputsystem script ini digunakan untuk menerima input dari inputsystem unity dan mengirimkan event.
2.utility -> EventContainer.cs && Utility.cs
pada Utility.cs script ini digunakan untuk menyimpan rumus yang akan digunakan untuk script lain 
dan EventContainer.cs digunakan untuk menampung event yang akan digunakan script lain
3.enum
menyimpan Enum
4.Tile -> Tile.cs(base), NormalTile.cs, BombTile.cs, AllColorTile.cs
Menyimpan data dan fungsi-fungsi untuk setiap tipe tile yang akan digunakan
5. Board
board untuk menampung dan menyimpan data dari kumpulan tile
6. Interface
interface


# Notes on any assumptions or design choices
saya menggunakan design ini untuk memudahkan dalam mengerjakan game ini dengan baik dan cepat dan memudahkan untuk memperbaiki jika ada bug yang ditemukan Ketika mencoba.


# List of completed features + bonus items (if any)

🧩 Core Requirements
1. Gameplay Mechanics
Static grid size: 7x7
5 different colored dots
Drag to connect adjacent dots of the same color using only horizontal or vertical lines, diagonals are not allowed. Dots cannot be reused within the same connection
3 or more connected dots should be matched and cleared. There should be a basic animation when dots are matched (e.g., scaling down)
2. Grid Refill System
Dots above fall down to fill empty spaces
New dots appear from the top
Include translate downward animation
3. Debugging Tool
Right-click a dot to change its color (cycle through all available colors)
4. Special Tile
Connect and clear 6 dots to create a Bomb at the end of the line. Bomb will explode on click and destroys a 3x3 area

🌟 Bonus Requirements (Optional)
1. More Special Tile
Connect and clear 9 dots to create a Colored Bomb. A Colored Bomb can be connected to a dot to clear all dots of that color from the grid
2. More Debugging Tool
Show a Shuffle button that rearranges the dots in the grid randomly
Press the Space key to highlight a possible connection for the player

