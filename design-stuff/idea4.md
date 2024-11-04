# Idea 4
> based discussion, semi-final idea 

## Concept
You are one of the hikers in the 50s in the Alps. In a heavy snowstorm you were separated from your group. A half closed world with 3 POIs you collect items nesseesary to escape the monster that is trying to kill you.
### Tone:
- dark, 
- mysterious, 
- with psychological horror elements

## References
### Story:
- [Wikipedia - Dachstein hiking disaster](https://en.wikipedia.org/wiki/Dachstein_hiking_disaster)
  - [What happened dachstein hiking disaster?](https://www.grunge.com/1577149/what-happened-dachstein-hiking-disaster/)
  - 13 people, 10 students and 3 teachers go hiking in 1954, all die, only 11 bodies found, 2 student missing forever
- [Wikipedia - English calamity](https://en.m.wikipedia.org/wiki/English_calamity)
  - kinda similar to what happened in germany in 1936
### Monster:
- [Wikipedia- Tatzelwurm](https://pl.wikipedia.org/wiki/Tatzelwurm) - cool monster
  - ![Tatzelwurm colorful](https://github.com/Bajtix/culture-jam-austria/blob/main/design-stuff/concepts/IMG_2261.png)
  - ![Tatzelwurm black and white](https://github.com/Bajtix/culture-jam-austria/blob/main/design-stuff/concepts/IMG_2262.png)

## Step by step story
1. During a hike in Alps you and your friends were cought by a heavy snowstrom.
2. After beeing separated from them in a blizzard you see a distant shilouette of the Tatzelwurm. Stumbling around, you find a dead body of one of your friends, visibly poisoned and torn apart by some strong claws.
3. You realise that you have to get out of the blizzard NOW, behind you, you find a steep slope leading down the mountain. You know it's too steep to run down, but you remember that some of your commrades had skis that you could use. (Question: Whe do you want to go down yourself instead of checking if maybe some of your friends are still alive?)
4. You take out a map with three POIs outlined in the forest. An old shed, a weird stone structure and hunter's box. You decide to go to them to find your skis. (Question: how do you know that you will find the skis next to those POIs?)
5. After collecting the skis you finally get to ski down the slope, with a chace sequence by the monster. If you escape you win, roll credist.

NOTE: After writing out the story I realised that there are a few plotholes in our idea. So I came up with an alteration to it that fixes most of them.

## Step by step story (altered)
0. Now the story is set at the modern age and you have a smartphone with you.
1. You wanted to follow the steps of 13 hikers that in 1954 mysteriously dissapeared in this area. You get cought in a heavy snowstorm.
2. You call emergency, they tell you that the weather where you are is too heavy to pick you up so, you yourslef need to get to the randevoux point. The operator will instruct you all the way.
3. Operator tells you to get down a steep slope, the only safe way to do this is to ski down. There are three POIs next to your location, an old shed, a weird stone structure and hunter's box. You can see all of them on your phone's map, together with your location. Instructor says to check those POIs out, they may be some skis lying around in them. (Question: this does not make 100% sense)
4. From the sfx point of view you constantly hear something from your phone. Despite terrifying forest and clear signs that something may lurk in it, the instructor constantly conforts you and says that there is nothing in the forest to be afraid of. His instructions also serve as gameplay tutorial.
5. After compleating some milestone (a short one, about 1/10th of the game, nothing more complex) the snowstorm gets significantly heavier and you suddenly loose contact with emergency. You realise that you phone is compleatly dead and does not respond. Suddenly a comforting voice dissapears and you are left alone with the sounds of the forest. Nevertheless, you already know what to do, so you continue.
6. Same as 5. from non alternative story.

## Monster mechanics
The monster very rarely shows up himself. Instead you constantly see effects of his existance. Scratches on the trees and footprints appear when you are away. Above the constant howl of the blizzard you hear the sounds of something big moving through dense vegetation, you sometimes can catch a shilouette that dissapears the moment you direcly look at it, etc. From the mechanics pov, monster is just a bunch of timers. If any of the timers run down to zero, game over. The lower the timer, the more anomalies caused by the monster you see.
There is one global timer and three local timers, one for every POI. Local timer of the POI tick down only if you are near that POI. POI timers are significanly shorter than the global timer, but they tick up when you are not in their POI. Each POI timer is also significanly shorter than estimated time required to finish the POI. POIs can be finished in any order.

This is the expected game loop:
1. You do some simple (among us like) tasks near an POI.
2. You can see more anomalies, the snowstorm is getting stronger. You can feel something approaching.
3. The indicators never tell you exactly how much time you have left, so you need to make a decision. You can either abbandon the POI now and relocate to another one risking redoing the task on hand (and wasting more global timer) or you can finish the thing you are doing first, and then move on risking getting caught at the POI.

