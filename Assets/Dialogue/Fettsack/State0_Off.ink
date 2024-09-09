INCLUDE ..\globals.ink

// Fettsack: State 0 : OFF

Hey Hey Bongo! Wie wär's mit einem Snack?
->main

=== main ===
Was würdest du lieber essen?
    *[Ein Schokoriegel] 
        Gute Wahl mein Freund! Haha! 
        -> snack
    *[Eine große Pizza!] 
        -> pizza
    *[Ein riesen Smoothie!] 
        -> smoothie
    
=== snack ===
Na dann frohes Kauen! Haha! #rage:-1
-> end

=== pizza ===
Ohho! Ich mach sofort die Bestellung fertig! #rage:-1
->end

=== smoothie  ===
Das Grünzeug kannst du dem Gärtner überlassen! Haha! #rage:1
    *[Smoothies sind gut für die Hüften] 
        Wolltest du damit etwas sagen? Haha!
        ->DONE
    *[Ich überlasse dir die Snacks] 
        Ein kleiner Biss für mich, ein großer für den Snackautomaten! Hahaha!
        ->DONE

=== end ===
Wenn ich so mit dir rede, muss ich die ganze Zeit an Essen denken.

Ein kleiner Biss für mich, ein großer für den Snackautomaten! Hahaha!

Bis bald, mein Freund!

-> END
