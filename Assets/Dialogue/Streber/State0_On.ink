INCLUDE ..\globals.ink

// Streber: State 0 : ON

Weißt du, am liebsten gehe ich am Wochenende fischen.
-> main

=== main ===
* [Oh wie schön!]   #rep:1
    -> freude
* [Wirklich?]       #rep:-1
    -> frage

=== freude ===
Ja. Die Natur ist bezaubernd am See.    #score:1
~ SwitchDialogueState("Off")
-> end

=== frage ===
Ja tatsächlich, es entspannt mich sehr. #score:-1
~ SwitchDialogueState("Off")
-> end

=== end ===
Nun denn, auf ein Neues!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
