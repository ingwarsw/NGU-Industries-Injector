# NGUIndustriesInjector

NGUIndustriesInjector is an automation platform for the steam version of [NGU INDUSTRIES](https://store.steampowered.com/app/1433990/NGU_INDUSTRIES/).

# Instructions

Releases can be found in the releases section of the github page [here](https://github.com/ingwarsw/NGU-Industries-Injector/releases). The latest release can be found [here](https://github.com/ingwarsw/NGU-Industries-Injector/releases/latest). Do not download the source code archive, instead download the zip file with the release version in the name (3.0.0.zip for example). Extract the downloaded archive to a directory of your choice. To start automation, run inject.bat in the extracted folder when NGU Idle is open. DO NOT RUN AS ADMINISTRATOR.

You'll know the injection has worked if you see the following in the upper left portion of NGU.

![Injected](https://i.imgur.com/JIF2pl4.png)

To upgrade to a new release, restart NGU idle and run the inject script again.

# Configuration

After injecting the dll, a new folder will be created on your Desktop called NGUIndustriesInjector. Settings files will be automatically written to this directory. The following files are of interest:

- **settings.json** - Contains settings used by the application. Most settings can be configured from the in-game menu.

A logs folder will also be created with the following files:

- **inject.log** - Logs general information about the injector
- **loot.log** - Logs all the loot dropped by enemies. Also logs money pit and daily spin results.
- **combat.log** - Logs information from the combat algorithm
- **allocation.log** - Logs information about allocating energy/magic/gear/diggers

Saving either file will automatically reload the settings in the configuration file. Reloading the game isn't necessary

# Settings Configuration

An in game menu can be opened using the F1 button.

![General Settings](https://i.imgur.com/Ru8YXTA.png)

- **Master Switch** - If turned off, will disable all automation

- **Auto Daily Spin** - Automatically do daily spin and log the result to the loot.log file

# Acknowledgements
Based on [NGUInjector](https://github.com/rvazarkar/NGUInjector) done by rvazarkar 
SharpMonoInjector can be found [here](https://github.com/warbler/SharpMonoInjector). This wouldn't be possible without the excellent work done there.
Obviously 4G for making NGU INDUSTRIES.
