INCLUDE ..\globals.ink

// Streber: State 1 : ON

Wusstest du, dass es eine effiziente Methode gibt, um Frösche von einer Heizung zu unterscheiden?
-> main

=== main ===
* [Äh, okay... Hast du heute Morgen genug Kaffee getrunken?] 
    -> antwort
* [Äh, okay... Hast du das auf dem Weg zum Mars gelernt?]          
    -> antwort

=== antwort ===
 #score:1
Nein, weißt du's nicht? Frösche hüpfen weg, wenn sie heiß werden!

-> end

=== end ===
Bis bald! Haha!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
