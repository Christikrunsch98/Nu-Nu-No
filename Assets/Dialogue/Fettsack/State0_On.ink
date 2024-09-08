INCLUDE ..\globals.ink

// Fettsack: State 0 : ON

Hey Hey Bongo! Wie wär's mit einem Snack?
->main

=== main ===
Was willst du Essen?
    *[Ein Schokoriegel] 
        Gute Wahl mein Freund! Haha! #rage:0 #rep:2    
        -> snack
    *[Eine große Pizza!]#rage:0 #rep:3
        -> pizza
    *[Ein riesen Smoothie!]#rage:-2 #rep:-1
        -> smoothie
    
=== snack ===
Na dann frohes Kauen! Haha!
-> end

=== pizza ===
Ohho! Ich mach sofort die Bestellung fertig!
->end

=== smoothie  ===
Nicht schlecht! Da wird einer wohl ganz schön grün hinter den Ohren! Haha!#rage:1 #rep:1
->END

=== end ===
Wenn ich so mit dir rede, muss ich die ganze Zeit an Essen denken.

Ein kleiner Biss für mich, ein großer für den Snackautomaten! Hahaha!

Bis bald, mein Freund!

-> END
