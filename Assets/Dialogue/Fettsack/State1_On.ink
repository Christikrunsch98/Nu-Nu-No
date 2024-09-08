INCLUDE ..\globals.ink

{fettsackRep > 0: ->goodRep| ->badRep}

// Fettsack: State 1 : ON
=== goodRep ===
Hey Bongo, weißt du wer heute Morgen die geniale Idee hatte Chickenwings zu bestellen? Haha!

ICH! argh

*[Glückwunsch.] #rage:0 #rep:-1
        -> end1
        
*[Klar, du bist der wahre Trendsetter. Was kommt als nächstes? Pommes mit Ketchup?]#rage:-1 #rep:2
        -> end2
        
=== end1 ===        
Hehe! Guter Gaumenschmaus du kleine Laus! Haha!
-> DONE

=== badRep ===
Hey mein Freund, na weißt du wer heute Morgen die geniale Idee hatte Chickenwings zu bestellen? Haha!

ICH! Haha!
*[Glückwunsch! Dann pass auf, dass du nicht wegfliegst!]
    ->end3

*[Klar, du bist der wahre Trendsetter. Was kommt als nächstes? Pommes mit Ketchup?]#rage:0 #rep:-1
        -> end2
        
=== end2 ===  
Spielverderber! Du weißt ja gar nicht, was gutes Gourmetessen ist!
-> DONE


=== end3 ===
Haha! Du machst ja ein auf witzig! Gefällt mir! #rage:-1 #rep:2

-> DONE
