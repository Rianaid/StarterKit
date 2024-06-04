# StarterKit
`Server side only` mod that allows you to issue different starter kits to new vampires.


<details>
<summary>Changelog</summary>

0.1.0
- Initial public release of the mod.

</details>

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Install [VampireCommandFramework](https://v-rising.thunderstore.io/package/deca/VampireCommandFramework/)
* Extract _StarterKit.dll_ into _(VRising server folder)/BepInEx/plugins_

## Commands

Only one:  `.kit [Kit Name]` - Give kit with same name. 

if you don't specify a name, it gives `startkit`

## Configurable Values
_BepInEx\config\StarterKit.cfg_
```ini
[StarterKit]

## Enable kit command..
# Setting type: Boolean
# Default value: true
EnableKitCommand = true

## Message when kit given player.
# Setting type: String
# Default value: Enjoy your <color=#ffffffff>free gear pack</color> and let the battle begin!
MessageOnGivenKit = Enjoy your <color=#ffffffff>free gear pack</color> and let the battle begin!

## Message when the player has already used a kit and will try to use it again.
# Setting type: String
# Default value: You have already used the starter kit.
MessageAlreadyUsedKit = You have already used the starter kit.
```
## StartKit.json
_BepInEx\config\StarterKit\StartKits.json_
```json
{
  "startkit": [
    {
      "Name": "Item_Boots_T09_Dracula_Brute",
      "Amount": 1
    },
    {
      "Name": "Item_Chest_T09_Dracula_Brute",
      "Amount": 1
    },
    {
      "Name": "Item_Gloves_T09_Dracula_Brute",
      "Amount": 1
    },
    {
      "Name": "Item_Legs_T09_Dracula_Brute",
      "Amount": 1
    }
  ]
}
```
### Credits

This mod idea was suggested by [@Vex](https://ideas.vrisingmods.com/posts/165/kits-system) on our community idea tracker. Please vote and suggest your ideas [here](https://ideas.vrisingmods.com/).

[V Rising Mod Community](https://discord.gg/vrisingmods) is the best community of mods for V Rising.

[@Deca](https://github.com/decaprime), thank you for the exceptional frameworks [VampireCommandFramework](https://github.com/decaprime/VampireCommandFramework).

