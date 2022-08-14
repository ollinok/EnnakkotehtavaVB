# Ennakkotehtävä - Olli Nokkonen

Luotu .NET Blazor -frameworkilla.

Kehitys lokaalisti Visual Studio 2022 Community Editionilla.
Tietokannasta tehtiin kopio ja ajettiin Docker-konttina kehitystyön ajaksi.
Käyttöliittymän suunnittelu tehty Figmalla. https://www.figma.com/

Tietokannan kuvaus ja Figmalla tehdyt käyttöliittymäpiirrokset löytyvät `/Muut tiedostot` -kansiosta.

Tuotantoversio ajettiin Docker-konttina Herokuun:
https://ennakkotehtava-olli-nokkonen.herokuapp.com/

```
heroku login
heroku container:login

#Rakenna kontti Dockerfilen avulla
heroku container:push -a ennakko-tehtava-olli-nokkonen web

heroku container:release -a ennakko-tehtava-olli-nokkonen web
```


## Kuvaus
Applikaatio Lääkefirma Oy:n työntekijöille. Kirjautunut työntekijä pystyy selaamaan asiakas- ja tuoteartikkelitietoja. Tuoteartikkeleiden hintoja on mahdollista muokata hintaryhmittäin.

## Toiminnot
- Käyttäjän todennus ja valtuutus sisäänkirjauksella käyttäen JsonWebTokeneja
- Tuoteartikkelien haku ja hintojen muokkaus
- Asiakkaiden haku ja tilausten seuranta
- Responsiivinen käyttöliittymä mobiilille
- Sessiomuistin hyödyntäminen tarpeellisesti tietokantakyselyissä

## Tekniikat
- .NET 6
- Blazor framework
- MariaDB
- Docker
- Bootstrap 5.1.3

## Suunnitellut lisätoiminnot
- API-rajapinta tilausten tekoon
