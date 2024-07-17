INCLUDE ..\globals.ink

// Streber: State 1 : ON

Es regnet gerade, merkst du das?
-> main

=== main ===
* [Ja, sehr beruhigend]
    -> freude
* [Wirklich?]
    -> frage

=== freude ===
Ja. Der Ton des prasselnden Regens ist ein Segen für meine Ohren.
-> end

=== frage ===
Ja, lausche mein Freund. Kannst du es hören?
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
