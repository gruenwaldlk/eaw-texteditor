# The Empire at War Text Editor

The Empire at War Text Editor is built for multi language projects and the work in distributed teams.
It supports the import and export from and to Petroglyph's `*.dat` files as well as a xml data structure that can be handled by any version control.

## The User Interface

When first opening the editor you will be presented with an mostly empty screen:

![Text Editor: Empty](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_001.jpg)

The three action buttons in the top toolbar provide access to the following functions, followed by the standard window actions of minimise, maximise and close.

* Import: Opens the [import dialogue](#the-import-dialogue)
* Export: Opens the [export dialogue](#the-export-dialogue)
* Toolbox: Opens the [Toolbox](#the-toolbox)

## The Import Dialogue

The first action you will have to perform is importing a file for editing.
Clicking either the **IMPORT** button in the window bar or the **IMPORT FROM FILE** button in the toolbox will bring up the import dialogue.

![Text Editor: Import](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_002.jpg)

The import dialogue allows you to either import a `*.xml` file or a `*.dat` file from disk and populate the translation table.
Clicking on the non-transparent overlay will cancel the dialogue.

![Text Editor: Loaded](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_003.jpg)

## The Export Dialogue

The export dialogue can be opened by either clicking the **EXPORT** button in the window bar or the **EXPORT TO FILE** button in the toolbox.

The export dialogue allows you to either export the translation data as `*.xml`, or as separate `*.dat` files for each loaded language.
You will only be able to pick a location to export to, the file names are determined by the editor as appropriate, if you are exporting to a non-empty directory any files with the same names will be overwritten.

![Text Editor: Export](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_004.jpg)

The exported `*.xml` file will always be called `TranslationManifest.xml`, the exported `*.dat` files will always be called `mastertextfile_language.dat`, where `language` will be replaced by the actual loaded languages.

Clicking on the non-transparent overlay will cancel the dialogue.

## The Toolbox

The toolbox provides access to various tools that can be used to your advantage.

![Text Editor: Export](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_005.jpg)

### Selected Language

A dropdown menu that allows you to pick the currently displayed language.

### ToDo View

The ToDo View is most useful for multi language projects, where multiple people work on translations.
It allows you to toggle a filter that will only display the translations that have been marked as to-do, either automatically, or manually.

![Text Editor: ToDo View Off](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_007.jpg)

![Text Editor: ToDo View On](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_006.jpg)

## Search

The search allows for simple key based or text based search queries.

![Text Editor: Search](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_008.jpg)

Toggling the advanced search on will allow you to use different search types:

* Case-sensitive search (Default: On)
* Simple search (Default)
* Wildcard search: A wildcard based search.
* RegEx (Regular Expression) based search.

### Wildcards

| Wildcard specifier | Matches                                   |
|:-------------------|:------------------------------------------|
| * (asterisk)       | Zero or more characters in that position. |
| ? (question mark)  | Zero or one character in that position.   |

## Import/Export

Provides access to the Import and Export dialogues via **IMPORT FROM FILE...** and **EXPORT TO FILE ...** buttons.

## Data Integrity

**VERIFY DATA INTEGRITY...** is most useful in multi-language projects. It will add all missing translations to all loaded languages. It will not automatically translate the missing items, but initially add them and mark them as to-do items.

## Editing a Translation

Double-clicking on an item in the translation table will bring up the edit dialogue.

![Text Editor: Edit Dialogue](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_009.jpg)

The dialogue allows you to edit the translation for all loaded languages at the same time, and is not dependent on the currently selected language.

Clicking on the semi-transparent overlay or hitting the <kbd>Esc</kbd> button will close the dialogue.

## Adding a New Translation

Right-clicking anywhere on the translation table will bring up the context menu, allowing you to edit the currently selected item or add a new translation via the new translation dialogue.

![Text Editor: Context Menu](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_010.jpg)

A new translation can only be added, if a valid text key is provided.Translations for all loaded languages can then be filled in. Translations left blank for loaded languages will be populated with the translation from the master language and marked as to-do item. 

![Text Editor: Add Menu: Invalid Key](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_011.jpg)
![Text Editor: Add Menu: Valid Key](https://documentation.yuuzhanvongatwar.com/img/doc_text_editor_012.jpg)

Hitting the **ADD** button will save the translation, pressing **CANCEL** will abort the change.

## Best Practices And FAQ

### How To Setup A Multi-language Project

Setting up a multi-language project requires some initial work, as the base text cannot be redistributed for legal reasons, but is pretty straight forward.

**The text editor only supports the languages that are officially supported by the game:**

* English
* French
* German
* Italian
* Spanish

1. Import the language specific text files one after another from `*.dat`:
    * `mastertextfile_english.dat`
    * `mastertextfile_french.dat`
    * `mastertextfile_german.dat`
    * `mastertextfile_italian.dat`
    * `mastertextfile_spanish.dat`
2. Once all languages have been imported, you have the ability to switch through the various languages via the language dropdown in the toolbox.
3. Optional: Use the *"VERIFY DATA INTEGRITY ..."* tool to verify all translations.

### How To Work With Languages That Are Not Officially Supported By The Game

The game only supports five languages. In order to display a different text language, you have to replace one of the existing languages with your new language.

#### Limitations

* The editor only supports characters that can be displayed as `UTF-8` and are valid within an XML context.
* The game only supports characters that can be encoded as `16-bit UTF-16LE`
* The editor officially **only** supports the five official base-game languages. There is no official support for other languages at the moment, and there are no plans to support other languages in the future.

#### Example

If you want to create a Russian translation, you will have to pick one of the languages from the list to be replaced:

* English (Not advised, as the text editor currently uses this language as master for generating To-Do items)
* French
* German
* Italian
* Spanish

Once you've picked a language, you can replace the translation of that language with your actual translation.
