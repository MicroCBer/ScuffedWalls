# ScuffedWalls
A command line tool for making Noodle Extensions 2.0 beat saber maps & modcharts.

This tool does not do the same thing as beatwalls.

Features:
 - Create custom events.
 - Import/Combine map objects from other map files
 - Append custom noodle data to map objects
 - Work without code
 - 3d model to wall/note/bomb converter with animation & color
 - Image to wall w/ compression
 - Text to Wall (using models & images)
 
 Usage:
  - Drag the map folder onto the program
  - Input the number of the map file to generate to (Will overwrite anything in this map file)
  - Input y/n for Autoimport (avoids complete overwrite)
  - Input y/n for Backup (creates backups of map and SW file)
  - Type in the generated SW file, saving refreshes the program automatically. Or hitting R in the console window.
  
*Windows will probably bother you about this being malware. If you dont trust it clone the repo and build it yourself.*

If everything doesnt work and your in a country that uses , as the decimal symbol, changing regional settings is a common fix.

More info on Functions & Practical usage can be found [`here`](https://github.com/thelightdesigner/ScuffedWalls/blob/main/Functions.md)

Intro and Setup video tutorial by #Rizthesnuggie2634 [`right here`](https://youtu.be/RrcQRQfaXAI)

More info on 3d modeling for wall conversion can be found [`here`](https://github.com/thelightdesigner/ScuffedWalls/blob/main/Blender%20Project.md)

More info on TextToWall images can be found [`here`](https://github.com/thelightdesigner/ScuffedWalls/blob/main/TextToWall.md)

Rizthesnuggie's full intro documentation can be found [`here`](https://drive.google.com/drive/folders/1aAUuv8Ycmf2LdSRvKYhfThY2tQhZxFYS?usp=sharing)

## Examples

 - [`Illuminate`](https://www.youtube.com/watch?v=lFL3Gjy15oc&t=1s)
 - [`Homesick`](https://www.youtube.com/watch?v=St3fSqj8SXc)
 - [`Gamecube Intro`](https://www.youtube.com/watch?v=0SVRM0cmUVE)
 - [`Try`](https://www.youtube.com/watch?v=fO4Z6OG5w_I)
 - [`Real or Lie`](https://www.youtube.com/watch?v=59X3Qb78-Es)
 - [`Exosphere`](https://www.youtube.com/watch?v=698L0vSI0no)
 - [`Rare`](https://www.youtube.com/watch?v=fQpDYL0If7U)

<img src="https://github.com/thelightdesigner/ScuffedWalls/blob/main/Readme/Try.jpg" alt="Try" width="500"/>

## For Developers

To create a function, clone the repo and navigate to ScuffedWalls -> Program -> Functions and create a new .cs file. All classes under the namespace ScuffedWalls.Functions that are decorated with the ScuffedFunction attribute will be populated as a function. The params string constructor is used to define the name or names of the function. Your class must inherit from SFunction which contains an array of parameters, the InstanceWorkspace and the GetParam method, and the virtual method "Run". The starting point for your code must be in an override of the virtual method "Run". If you use a parameter without calling GetParam you must mark the parameter as used by setting WasUsed to true. InstanceWorkspace contains the lists of mapobjects that will be combined on finish.
