INCLUDE globals.ink

{ pokemonName == "": ->main | ->already_chose }

=== main ===
Which pokemon do you choose?
    +[Charmander]
        -> chosen("Charmander")
    +[Bulbasaur]
        -> chosen("Bulbasaur")
    +[Squirtle]
        -> chosen("Squirtle")

=== chosen(pokemon) ===
~ pokemonName = pokemon
You chose {pokemon}!
->END

=== already_chose ===
You already chose {pokemonName}!
->END