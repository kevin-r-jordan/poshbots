# Poshbots (PowerShell Bots)
Test your PowerShell skills by having competing against other PoshBots!

## Concept
Poshbots was created as a way to help my systems engineering team learn the basics of PowerShell. After logging into your account you could create one or more Poshbots and them battle other team members bots.

The two bots are placed on a 25 x 25 grid of squares. Each square is worth a random number of points and can be captured by moving your bot onto the square. 

Each turn the player could perform an action using PowerShell like syntax to either:
- *Get-Surroundings*: returns the value of the nearby squares, if they've been captured, and by whom.
- *Get-Score*: See if you're winning or losing and by how much.
- *Move-Up*: Move your bot up one square.
- *Move-Down*: Move your bot down one square.
- *Move-Left*: Move your bot to the left one square.
- *Move-Right*: Move your bot to the right one square.

Essentially, the bot with the best algorithm would win. Different strategies were implemented by different people on the team such as random movement, find and follow the competitor, or doing calculations of nearby squares to determine the optimal path. 

## Training Mode
Training mode allowed players to test their bots by calling the games API's directly through PowerShell and controller both their bot and a competing bot.

## Battle Mode
After a player trained their bot, they could compete against any of the other team member's bots that had been saved. Each bot's code was uploaded to an Azure function and then executed. Players could watch the outcome of the battle on a web UI in real time.