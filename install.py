import shutil
import os
import re
import json
import zipfile
import pathlib

DEV_DIR = 'C:\\Users\\novak\\AppData\\Roaming\\Thunderstore Mod Manager\\DataFolder\\LethalCompany\\profiles\\DEV\\BepInEx\\plugins\\teheidoma-LethalUkraine\\'
BUILD_DIR = 'C:\\Users\\novak\\RiderProjects\\LethalUkraine\\obj\\Release\\netstandard2.1\\'
RELEASE_DIR = 'C:\\Users\\novak\\RiderProjects\\release\\'

DLL_FILE = 'LethalUkraine.dll'
RESOURCE_DIR = 'C:\\Users\\novak\\LethalCompanyUnityTemplate\\AssetBundles\\StandaloneWindows\\'
RESOURCE_FILE = 'lethalukraine'


def get_plugin_version():
    with open("LethalUkraine/LethalUkrainePlugin.cs") as f:
        m = re.search('\[BepInPlugin\("\w+", "\w+", "(\d.\d.\d)"\)\]', f.read())
        if m is not None:
            return m.group(1)
        else:
            raise Excpetion('can\'t find version')


def copy(fr, to):
    print(f'copy {fr} to {to}')
    shutil.copyfile(fr, to)


os.system(
    ' C:\\Users\\novak\\Downloads\\NetcodePatcher-3.3.4-win-x64\\NetcodePatcher.Cli.exe .\\LethalUkraine\\bin\\Release\\netstandard2.1\\LethalUkraine.dll .\\NetcodePatcher\\')
copy(BUILD_DIR + DLL_FILE, DEV_DIR + DLL_FILE)
copy(BUILD_DIR + DLL_FILE, RELEASE_DIR + DLL_FILE)

copy(RESOURCE_DIR + RESOURCE_FILE, RELEASE_DIR + RESOURCE_FILE)
copy(RESOURCE_DIR + RESOURCE_FILE, DEV_DIR + RESOURCE_FILE)

MANIFEST = {
    "name": "LethalUkraine",
    "version_number": get_plugin_version(),
    "website_url": "https://github.com/teheidoma/LethalUkraine",
    "description": "YOUUU FINALLY SOME UKRAINE IN MY LETHAL",
    "dependencies": [
        "Evaisa-LethalLib-0.13.2"
    ]
}

with open('release/manifest.json', 'w') as f:
    json.dump(MANIFEST, f, indent=4)

with zipfile.ZipFile(f'release/{get_plugin_version()}.zip', mode='w') as a:
    for file_path in pathlib.Path('release/').iterdir():
        if 'zip' not in file_path.name:
            a.write(file_path, arcname=file_path.name)
