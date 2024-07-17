INCLUDE ..\globals.ink

// Streber: State 3 : ON

Hey! Weißt du wo mein Sandwich ist!?
-> main

=== main ===
* [Beruhig Dich!]
    -> A
* [In der Küche?]
    -> B

=== A ===
NEIN! Ohne mein Sandwich geht gar nicht! Ich kann so niemals arbeiten!
-> end

=== B ===
OMG! DU GENIE! Ich werde direkt mal nachschauen!
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
