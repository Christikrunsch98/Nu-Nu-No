INCLUDE ..\globals.ink

// Chef: State 2 : OFF
Ich hoffe die Rede vorhin hat dir eingeleuchtet!
-> main

=== main ===
Was für Themen gibt es?
*[Kundenbetreuung] 
    Das hat gerade keinen Platz! Kümmer Dich mit den anderen darum!
    -> main 
*[Gehaltsfragen] 
    ->ant0
*[Urlaub buchen] 
    ->ant1

=== ant0 ===
Vergiss es! In 10 Jahren vielleicht!
*[Dann werde ich meine Leistung weiter steigern.]
    Hm, das will ich sehen. Vielleicht reden wir dann nochmal. #rage:-2
    -> end
*[Das ist enttäuschend... Ich hatte gehofft, Sie würden meine Leistung anerkennen]
    Anerkennung? Du kriegst dein Gehalt, und das ist Anerkennung genug! #rage:1
    -> end
    
=== ant1 ===
Vergiss es!
*[Gesetzlich habe ich Anspruch darauf.]
    Das Gesetz interessiert mich erst dann, wenn Sie was leisten! Frechheit! #rage:2
    ->end
*[Ich möchte für das Team in Bestform sein.] 
    Guter Punkt... Kommen Sie wieder. #rage:-3
    -> end
    
=== end ===
Noch weitere Fragen?
~SwitchDialogueState("Off")
-> DONE
