import shutil
import os 

DEV_DIR = 'C:\\Users\\novak\\AppData\\Roaming\\Thunderstore Mod Manager\\DataFolder\\LethalCompany\\profiles\\DEV\\BepInEx\\plugins\\teheidoma-LethalUkraine\\'
BUILD_DIR = 'C:\\Users\\novak\\RiderProjects\\LethalUkraine\\obj\\Release\\netstandard2.1\\'
RELEASE_DIR = 'C:\\Users\\novak\\RiderProjects\\release\\'

DLL_FILE = 'LethalUkraine.dll'
RESOURCE_DIR = 'C:\\Users\\novak\\LethalCompanyUnityTemplate\\AssetBundles\\StandaloneWindows\\'
RESOURCE_FILE = 'lethalukraine'

def copy(fr, to):
    print(f'copy {fr} to {to}')
    shutil.copyfile(fr, to)


os.system(' C:\\Users\\novak\\Downloads\\NetcodePatcher-3.3.4-win-x64\\NetcodePatcher.Cli.exe .\\LethalUkraine\\bin\\Release\\netstandard2.1\\LethalUkraine.dll .\\NetcodePatcher\\')
copy(BUILD_DIR + DLL_FILE, DEV_DIR+ DLL_FILE)
copy(BUILD_DIR + DLL_FILE, RELEASE_DIR+ DLL_FILE)

copy(RESOURCE_DIR + RESOURCE_FILE, RELEASE_DIR + RESOURCE_FILE)
copy(RESOURCE_DIR + RESOURCE_FILE, DEV_DIR + RESOURCE_FILE)