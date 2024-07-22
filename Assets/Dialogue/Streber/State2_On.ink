INCLUDE ..\globals.ink

// Streber: State 2 : ON

Hey, weißt du wie dieser Drucker funktioniert?
-> main

=== main ===
* [Brauchst du Hilfe?]  #rep:-1
    -> ja
* [Nein, wie denn?]     #rep:1
    -> nein

=== ja ===
 #score:-1
Nein nein mein Freund. Ich komme klar.
-> end

=== nein ===
 #score:1
Eine kleine Drüse platzier Tropfen für Tropfen feinste Tinte auf das Blatt.
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
