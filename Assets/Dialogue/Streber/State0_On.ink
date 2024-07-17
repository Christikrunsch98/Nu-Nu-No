INCLUDE ..\globals.ink

// Streber: State 0 : ON

Weißt du, am liebsten gehe ich am Wochenende fischen.
-> main

=== main ===
* [Oh wie schön!]
    -> freude
* [Wirklich?]
    -> frage

=== freude ===
Ja. Die Natur ist bezaubernd am See.
-> end

=== frage ===
Ja tatsächlich, es entspannt mich sehr.
-> end

=== end ===
Nun denn, auf ein Neues!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
