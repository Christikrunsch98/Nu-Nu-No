INCLUDE ..\globals.ink

// Fettsack: State 1 : ON

Hey Bongo, weißt du wer heute Morgen die geniale Idee hatte Chickenwings zu bestellen? Haha!

ICH! argh

*[Glückwunsch.] 
        -> end1
        
*[Klar, du bist der wahre Trendsetter. Was kommt als nächstes? Pommes mit Ketchup?]
        -> end2
        
=== end1 ===        
Hehe! Guter Gaumenschmaus du kleine Laus! Haha!
~ SwitchDialogueState("Off")
-> DONE

        
=== end2 ===  
Spielverderber! Du weißt ja gar nicht, was gutes Gourmetessen ist!
~ SwitchDialogueState("Off")
-> DONE