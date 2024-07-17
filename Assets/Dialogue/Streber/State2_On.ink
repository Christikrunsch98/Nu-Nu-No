INCLUDE ..\globals.ink

// Streber: State 2 : ON

Hey, weißt du wie dieser Drucker funktioniert?
-> main

=== main ===
* [Brauchst du Hilfe?]
    -> ja
* [Nein, wie denn?]
    -> nein

=== ja ===
Nein nein mein Freund. Ich komme klar.
-> end

=== nein ===
Eine kleine Drüse platzier Tropfen für Tropfen feinste Tinte auf das Blatt.
-> end

=== end ===
Bis bald!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
