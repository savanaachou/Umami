VAR playerName = ""

The neon outside hums, flickering in puddles on the cracked street. The shop smells faintly of simmering broth.  

It’s quiet as usual… until you hear footsteps approaching.

A young woman in a long coat steps in, shaking off rain droplets.

Saeko: *notebook in hand* “Evening… didn’t expect anyone to open at this hour.”

{playerName}: "That's the point."

Saeko: *studying you carefully* “You don't look like you are from around here.”

{playerName}: “I am. Just… been gone.”

Saeko: “You’ve been away a long time?”

{playerName}: “…Something like that.”

Saeko: *tilts head, slightly amused*  
“So that explains it. You’ve got the look of someone who doesn’t know what kind of mess the city’s in now.”

She steps closer, sliding onto a stool. She orders Shoyu Ramen with thin straight noodles, the way someone orders something familiar, not random.

Saeko’s eyes move across the old posters and worn décor.

-> saeko_begin_cooking


=== saeko_begin_cooking ===
order #order:Saeko

-> saeko_post_cook


=== saeko_post_cook ===
#serve:Saeko

As you prepare her bowl, Saeko watches the steam rise.

Saeko: “City’s not doing so well. Gangs are restless. Government’s… stretched thin.”

Saeko: “And you... you’re here, running a shop in the middle of it all. Bold move.”

{playerName}: "..."

Saeko looks at you, not unkindly, but assessing.

-> saeko_choice_1


=== saeko_choice_1 ===

+ “I just want to make ramen. Nothing else.”
    #rel:Saeko 1
    Saeko: *raises an eyebrow, faint smile* “Simple. Honest. Not many left who care about the small things.”
    -> saeko_continue_1

+ “I’ve got nothing to say about the city.”
    Saeko: *smirks faintly* “Fair enough. Keeping your head down... Still, be careful.”
    -> saeko_continue_1


=== saeko_continue_1 ===

Saeko begins eating quietly.  

She glances between the window and you, thoughtful, like she’s measuring the atmosphere… and the person running it.

After a moment:

Saeko: “You seem steady. Capable. The kind of person who can handle delicate matters.”

{playerName}: “What do you mean by that?”

Saeko: *shrugs lightly* “I need to know I can count on a few neutral spots like this. Places where things don’t escalate.”

-> saeko_choice_2


=== saeko_choice_2 ===

+ “You can count on me.”
    #rel:Saeko 1
    Saeko: *expression softens, tone warming* “Good. Discretion is rare these days. Maybe… we’ll see what else we can learn from each other.”
    -> saeko_exit

+ “I’m just running a shop. Don’t expect anything more.”
    Saeko: *nods, slightly skeptical* “Fair. Mind your own business if you want. But remember… your choices affect the city more than you think.”
    -> saeko_exit


=== saeko_exit ===

Saeko finishes her ramen, places the bowl down neatly, and leaves a folded bill under the chopsticks.

She stands, pauses, and looks back.

Saeko: “That was good. Thank you. It reminded me of something… something I haven’t felt in a while.”

Saeko: “I’ll be back. I have a feeling this place offers more than it seems.”

She steps into the rainy night, disappearing beneath the flickering neon.

-> DONE
