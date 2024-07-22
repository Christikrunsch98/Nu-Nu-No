INCLUDE ..\globals.ink

// Streber: State 3 : ON

Hey! Weißt du wo mein Sandwich ist!?
-> main

=== main ===
* [Beruhig Dich!]   #rep:-2
    -> A
* [In der Küche?]   #rep:2
    -> B

=== A ===
 #score:-2
NEIN! Ohne mein Sandwich geht gar nicht! Ich kann so niemals arbeiten!
-> end

=== B ===
 #score:2
OMG! DU GENIE! Ich werde direkt mal nachschauen!
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
