INCLUDE ..\globals.ink

// Streber: State 1 : ON

Es regnet gerade, merkst du das?
-> main

=== main ===
* [Ja, sehr beruhigend] #rep:1
    -> freude
* [Wirklich?]           #rep:-1
    -> frage

=== freude ===
 #score:1
Ja. Der Ton des prasselnden Regens ist ein Segen für meine Ohren.
-> end

=== frage ===
 #score:-1
Ja, lausche mein Freund. Kannst du es hören?
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
