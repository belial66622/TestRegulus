# TestRegulus
 
Dalam game ini ada beberapa script yang saya gunakan:<br>
1.input -> InputControl.cs<br>
Input yang saya gunakan adalah newinputsystem script ini digunakan untuk menerima input dari inputsystem unity dan mengirimkan event.<br>
2.utility -> EventContainer.cs && Utility.cs<br>
pada Utility.cs script ini digunakan untuk menyimpan rumus yang akan digunakan untuk script lain <br>
dan EventContainer.cs digunakan untuk menampung event yang akan digunakan script lain<br>
3.enum<br>
menyimpan Enum<br>
4.Tile -> Tile.cs(base), NormalTile.cs, BombTile.cs, AllColorTile.cs<br>
Menyimpan data dan fungsi-fungsi untuk setiap tipe tile yang akan digunakan<br>
5. Board<br>
board untuk menampung dan menyimpan data dari kumpulan tile<br>
6. Interface<br>
interface<br>


# Notes on any assumptions or design choices
saya menggunakan design ini untuk memudahkan dalam mengerjakan game ini dengan baik dan cepat dan memudahkan untuk memperbaiki jika ada bug yang ditemukan Ketika mencoba.


# List of completed features + bonus items (if any)

🧩 Core Requirements
1. Gameplay Mechanics<br>
Static grid size: 7x7<br>
5 different colored dots<br>
Drag to connect adjacent dots of the same color using only horizontal or vertical lines, diagonals are not allowed. Dots cannot be reused within the same connection<br>
3 or more connected dots should be matched and cleared. There should be a basic animation when dots are matched (e.g., scaling down)<br>
2. Grid Refill System<br>
Dots above fall down to fill empty spaces<br>
New dots appear from the top<br>
Include translate downward animation
3. Debugging Tool<br>
Right-click a dot to change its color (cycle through all available colors)<br>
4. Special Tile<br>
Connect and clear 6 dots to create a Bomb at the end of the line. Bomb will explode on click and destroys a 3x3 area<br>

🌟 Bonus Requirements (Optional)<br>
1. More Special Tile<br>
Connect and clear 9 dots to create a Colored Bomb. A Colored Bomb can be connected to a dot to clear all dots of that color from the grid<br>
2. More Debugging Tool<br>
Show a Shuffle button that rearranges the dots in the grid randomly<br>
Press the Space key to highlight a possible connection for the player<br>

