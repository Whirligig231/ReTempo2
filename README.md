# ReTempo 2 α.1.0

ReTempo 2 is intended to be a GUI-based audio editor for altering the rhythms in audio samples based on the positions of beats in the sample. As it stands, very little of the intended functionality is present; the current version can only edit beatmaps.

ReTempo 2 is licensed under the GNU GPL v2; see COPYING.txt for details.

## The Beatmap Editor

The Beatmap Editor is currently the main window of ReTempo 2. It allows you to manipulate **RTBM files**, which are ReTempo 2's beatmap format.

### File Menu

The **File** menu gives you commands to save and load files.

* **File -> New... (Ctrl+N)**: Creates a new beatmap. You will be asked to load a sample to use as the audio. **Supported audio formats currently include AAC, MP3, M4A, WAV, and OGG.**
* **File -> Open... (Ctrl+O)**: Opens an existing beatmap on your computer. A beatmap is an **RTBM file**, which contains both the audio sample in use and the beats labeled within it.
* **File -> Save (Ctrl+S)**: Saves the current beatmap. If it has already been saved, it will overwrite the most recent file saved to.
* **File -> Save As... (Ctrl+Shift+S)**: Saves the current beatmap, prompting the user for a file name to save to.
* **File -> Import Sample... (Ctrl+I)**: Loads a new audio sample to use with the existing beatmap. **(NOTE: If the new sample is shorter, any beats that run over the length of the new sample will be removed. This operation cannot be undone.)**
* **File -> Reload Sample (Ctrl+R)**: Reloads the current audio sample from disk. **(NOTE: If the new version is shorter, any beats that run over the length of the new version will be removed. This operation cannot be undone.)** **(NOTE: This operation will fail if the file was loaded from the disk, as the location of the original sample is not saved in the RTBM format. Use File -> Import Sample... (Ctrl+I) to reload the sample in this case.)**
* **File -> Close Window (Ctrl+W)**: Closes the window (and, for now, the program).

### Edit Menu

The **Edit** menu gives you commands to edit the beatmap.

* **Edit -> Undo (Ctrl+Z)**: Undoes the most recent operation.
* **Edit -> Redo (Ctrl+Y)**: Redoes the most recent operation.
* **Edit -> Cut (Ctrl+X)**: Copies the selected beats to the clipboard and deletes them. Beats are copied as a space-delimited ordered list of time values, in seconds.
* **Edit -> Copy (Ctrl+C)**: Copies the selected beats to the clipboard without deleting them.
* **Edit -> Paste (Ctrl+V)**: Pastes the beats from the clipboard at the current playhead. If the clipboard is longer than the current selection, the selection will be extended. Beats in between the beats to be pasted will be deleted.
* **Edit -> Delete (Del)**: Deletes the beats in the current selection.
* **Edit -> Select All (Ctrl+A)**: Selects the entire sample.

### Playhead Menu

The **Playhead** menu gives you commands that move the playhead. The playhead marks the current play location and the start of the selection.

* **Playhead -> Play/Stop (Space)**: Plays or stops the sample, with clicks on each beat.
* **Playhead -> Restart Playback (Shift+Space)**: Plays the sample. If the sample was already playing, this will restart from the playhead.
* **Playhead -> To Start (Ctrl+Left)**: Moves the playhead to the start of the sample.
* **Playhead -> To End (Ctrl+Right)**: Moves the playhead to the end of the sample.
* **Playhead -> To Prev Frame (Left)**: Moves the playhead to the previous frame (sample of audio, 1/44100th of a second).
* **Playhead -> To Next Frame (Right)**: Moves the playhead to the next frame.
* **Playhead -> To Prev Beat (Shift+Left)**: Moves the playhead to the previous beat.
* **Playhead -> To Next Beat (Shift+Right)**: Moves the playhead to the next beat.

### Tools Menu

The **Tools** menu gives you commands that manipulate large numbers of beats at once.

* **Tools -> Map Beats by Tempo... (Ctrl+M)**: Opens the **Map Beats by Tempo** dialog; see below.
* **Tools -> Detect Beats (Ctrl+B)**: Clears the current selection and instead uses **[BeatRoot](https://code.soundsoftware.ac.uk/projects/beatroot-vamp)** to detect beats within the selection.

### Help Menu

The **Help** menu gives you commands to access documentation.

* **Help -> Show README (F1)**: Shows this README file.
* **Help -> About...**: Shows the About dialog box.

### Tool Buttons

The buttons underneath the menu bar give easy access to several common operations.

* **Import Sample button**: Provides the same functionality as **File -> Import Sample... (Ctrl+I)**.
* **Play button**: Provides the same functionality as **Playhead -> Restart Playback (Shift+Space)**.
* **Stop button**: Stops playback of the sample. This does nothing if the sample is already playing.
* **Seek to Start button**: Provides the same functionality as **Playhead -> To Start (Ctrl+Left)**.
* **Map Beats by Tempo button**: Provides the same functionality as **Tools -> Map Beats by Tempo... (Ctrl+M)**.
* **Detect Beats button**: Provides the same functionality as **Tools -> Detect Beats (Ctrl+B)**.

### Audio Display Area

This area displays the current sample as well as any beats on it. It can be clicked in various ways to manipulate beats and the current selection.

* The **blue waveforms** represent the left and right channels of the current sample on the top and bottom, respectively.
* The **darkened area** is the current selection.
* The **red lines** are beats in the current beatmap.
* The **black line** is the playhead, the start of the current selection.
* The **green line** is the current location being heard when the song is played. (Should *this* be called the playhead? Yeah, probably, but oh well.)

You can manipulate the audio display area in the following ways:

* Use the **scroll wheel** to zoom in and out.
* **Left-click** to place the playhead.
* **Left-click and drag on the playhead** to move it.
* **Left-click and drag on the end of the selection** to move it.
* **Left-click and drag elsewhere** to create a new selection.
* **Right-click an empty spot** to create a new beat.
* **Right-click and drag a beat** to move it.
* **Right-click a beat without dragging** to delete it.
* Use the **scroll bar under the audio display area** to move the viewport left and right in the sample.

## The Map Beats by Tempo Dialog

This dialog allows you to create equally-spaced beats within the selection. When the dialog appears, the text at the top will tell you how long the current selection is, in seconds. If there are already beats in the selection, the **Beats** and **BPM** fields will be automatically populated with estimates assuming your goal is to regularize the existing beats.

* **Beats field**: Here you can type the number of beats long your selection is supposed to be. This is not necessarily an integer. This refers to the *length* of the selection in beats at the desired tempo, not the *number of beat ticks* within the selection. (For instance, a selection 16.01 beats long will have 17 beats marked in it.)
* **BPM field**: Here you can type the intended tempo of your selection in BPM.

When you hit **OK**, the selection will be populated with beats according to the fields. Note that the playhead, i.e. the start of the selection, is *always* assumed to be the position of the first beat marker. Hit **Cancel** to cancel the operation and return to the Beatmap Editor.
