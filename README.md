# MemoryGame_WindowsForm
This project, Project_03_Memory_Game, is a classic memory card matching game implemented in C# using Windows Forms. The game challenges players to find matching pairs of cards within a limited time. It includes features like card shuffling, a countdown timer, score tracking, and sound effects for correct and incorrect matches. The game also provides options for customization, such as changing the background image, timer duration, and color schemes.

Key Features
Card Matching:

The game consists of 24 cards (12 pairs) with images on one side and a default back image.

Players click on two cards to reveal their images. If the images match, the cards remain face-up. If not, they flip back after a short delay.

Shuffling:

The cards are shuffled at the start of the game and after each restart to ensure a unique experience every time.

Timer:

A countdown timer is displayed, and players must find all matching pairs before the time runs out.

The timer can be customized to 45, 60, 90, 120, or 180 seconds.

Score Tracking:

Players earn points for each correct match. The score is displayed on the screen.

Sound Effects:

Sound effects play for correct matches, incorrect matches, and during the countdown when the timer is low.

Win/Lose Conditions:

If the player matches all pairs before the timer runs out, they win and are prompted to play again.

If the timer runs out before all pairs are matched, the player loses and is prompted to restart.

Customization Options:

Players can change the background image of the game.

They can also customize the colors of the title, score, and timer labels.

Pause/Resume:

The game can be paused and resumed, allowing players to take breaks.

Restart:

Players can restart the game at any time, resetting the timer, score, and card positions.

How It Works
Initialization:

The game initializes by loading all PictureBox controls into a list and shuffling their positions.

Gameplay:

Players click on two cards to reveal their images.

If the images match, the cards remain face-up, and the score increases.

If the images do not match, the cards flip back after a short delay.

Timer:

The countdown timer starts when the game begins and stops when the game is paused or ends.

When the timer reaches 10 seconds, the countdown sound effect plays, and the timer label turns red.

Win/Lose:

If all pairs are matched before the timer runs out, the player wins and is prompted to play again.

If the timer runs out, the player loses and is prompted to restart.

Customization:

Players can change the background image and colors of the title, score, and timer labels through the menu options.

Technical Details
Data Structures:

A List<PictureBox> is used to store and manage the cards.

Tags are used to identify and compare card images.

Randomization:

The Random class is used to shuffle the cards by swapping their positions.

Sound Effects:

The SoundPlayer class is used to play sound effects for correct matches, incorrect matches, and the countdown.

Timers:

Two timers are used:

One for the countdown and end-game logic.

Another to control access to the cards during gameplay.

Event Handling:

Click events on the PictureBox controls trigger the card-flipping logic.

Menu item clicks handle customization options like changing colors and background images.

Customization Options
Background Images:

Players can choose from multiple background images (e.g., Hunter, Hisoka, Kilua, etc.).

Timer Duration:

Players can select from predefined timer durations (45, 60, 90, 120, or 180 seconds).

Color Customization:

Players can change the colors of the title, score, and timer labels using a color dialog.
