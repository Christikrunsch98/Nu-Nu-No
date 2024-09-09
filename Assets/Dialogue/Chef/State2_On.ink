INCLUDE ..\globals.ink

// Chef: State 2 : ON
An die Arbeit!
*[Ja okay] 
    Ich sag's nicht noch einmal! #rage:1
*[Wie bitte?] 
    Ich sag's nicht noch einmal! #rage:2
~ SwitchDialogueState("Off")
-> DONE
