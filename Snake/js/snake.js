// Deklarerar canvas och storlek
var canvas = document.querySelector('canvas');
var c = canvas.getContext('2d');
canvas.width = 350;
canvas.height = 350;

// Deklarerar globala variabler
var move, power, enemySnake, obstacleMove;
var powerType = 0;
var button = document.getElementById('button');
var pointsText = document.getElementById("points")
var points = 0;
var foodTimer;
var timer = 0;
var powerTimer = 0;
var brownPower = 0;
var speed = 150;
var stop = 0;
var walls = true;
var enemy = false;
var movingWalls = false;

var grass = document.getElementById("grass");
var sushi = document.getElementById("sushi");

//------------------------------------------------------------------------------------------------
// Local storage
var retrievedData = localStorage.getItem("points");
var highscores = JSON.parse(retrievedData);
if (highscores == null) {
  var highscores = [0, 0, 0, 0, 0];
  localStorage.setItem("points", JSON.stringify(highscores));
}
// storing our array as a string
localStorage.setItem("points", JSON.stringify(highscores));
highscores.sort();

document.getElementById('highscore').innerHTML = "1: " + highscores[highscores.length - 1] + "<br />" + "2: " + highscores[highscores.length - 2] + "<br />" + "3: " + highscores[highscores.length - 3] + "<br />" + "4: " + highscores[highscores.length - 4] + "<br />" + "5: " + highscores[highscores.length - 5];

// Sätter en eventlistener på varje radio button
// Då någon av dem blir klickade byts värdet på färgen
var radio = document.forms['form'].elements['skin'];
for (var i = 0, len = radio.length; i < len; i++) {
  radio[i].onclick = function() {
    snake.color = this.value;
    this.blur();
    snake.draw();
  };
}

// Eventlistener för checkbox som toggle på väggar
document.getElementById("walls").addEventListener("click", function() {
  walls = !walls;
});

// Eventlistener för checkbox som toggle fiende orm
document.getElementById("enemy").addEventListener("click", function() {
  enemy = !enemy;
});

// Eventlistener för checkbox som toggle målsökande hinder
document.getElementById("obstacle").addEventListener("click", function() {
  movingWalls = !movingWalls;
});

// Eventlistener på paus/start knappen
// Funktionen stannar/startar några loopar
button.addEventListener('click', function() {
  if (button.innerHTML == "Start") {
    button.innerHTML = "Pause";
    snake.move();
    if (movingWalls == true) {
      obstacles[0].move();
    }
    clearInterval(foodTimer);
    timer = 0;
    foodTimer = setInterval(foodTimeout, 1000);
    if (powerType == 1) {
      power = setInterval(powerUpdate, 1000)
    }
  } else if (button.innerHTML == "Pause") {
    button.innerHTML = "Start"
    if (movingWalls == true) {
      clearTimeout(obstacleMove);
    }
    clearInterval(foodTimer);
    timer = 0;
    clearTimeout(move);
    if (powerType == 1) {
      clearInterval(power);
    }
  }
})

// Funktion som håller koll på hur länge frukten har varit ute
function foodTimeout() {
  timer++;
  if (timer == 10) {
    snake.snakeLength--;
    fruit.createFruit();
    timer = 0;
  }
  document.getElementById("foodTimer").innerHTML = "" + 10 - timer + " Sekunder";
};

// Funktion som visar hur länge powerup effekten varar
function powerUpdate() {
  document.getElementById("powerup").innerHTML = "" + brownPower - powerTimer + " Sekunder";
  powerTimer++;
};

// Snake objekt
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
function Snake(dx, dy, size, snakeLength) {
  // Deklarerar lokala variabler
  var direction;
  var enemyClear = 0;

  this.x = [];
  this.y = [];
  this.color = "red";
  this.points = points;
  this.dy = dy;
  this.dx = dx;
  this.size = size;
  this.snakeLength = snakeLength;

  // Funktion  för att skapa ormen vid startpunkten
  this.createSnake = function() {
    var start = 60;
    c.beginPath();
    c.strokeStyle = "black";
    c.fillStyle = this.color;
    for (var i = 0; i < snakeLength; i++) {
      this.x[i] = start - i * 10;
      this.y[i] = start;
      c.strokeRect(this.x[i], this.y[i], size, size);
      c.fillRect(this.x[i], this.y[i], size, size);
    }
    this.draw();
  }

  // Rit funktion som ritar ut ormen
  this.draw = function() {
    c.beginPath();
    c.strokeStyle = "black";
    if (this.color == "grass") {
      c.fillStyle = c.createPattern(grass, "repeat");
    } else if (this.color == "sushi") {
      c.fillStyle = c.createPattern(sushi, "repeat");
    } else {
      c.fillStyle = this.color;
    }
    for (var i = 0; i < snakeLength; i++) {
      c.strokeRect(this.x[i], this.y[i], size, size);
      c.fillRect(this.x[i], this.y[i], size, size);
    }
    fruit.draw();
  };

  // Funktion som kollar om spelar ormen är död
  // Ormen för genom att kalla på reset funktionen
  this.dead = function() {
    // Om spelaren kör med väggar ska ormen dö
    if (walls == true) {
      if (this != enemySnake) {
        if (this.x[0] >= c.canvas.height || this.x[0] <= -size || this.y[0] >= c.canvas.height || this.y[0] <= -size) {
          this.reset();
        }
      }
    }

    // Om ormen krymper till inget dör den
    if (snakeLength == 0) {
      this.reset();
    }

    // Om spelaren kör utan väggar kommer ormen ut från andra sidan
    // Detta gäller alltid för fiende ormen
    if (this.x[0] <= -size) {
      this.x[0] += c.canvas.width;
    }

    if (this.x[0] >= c.canvas.width) {
      this.x[0] = 0;
    }

    if (this.y[0] >= c.canvas.height) {
      this.y[0] = 0;
    }

    if (this.y[0] <= -size) {
      this.y[0] += c.canvas.height;
    }

    // Kollar om ormen krockar med hindren
    // Kollar också om en powerup är under effekt då den skippar detta
    // Startar om powerup effekten när den är klar också
    if (powerTimer < 1 && powerType != 1) {
      for (var i = 0; i < obstacles.length; i++) {
        if (this.x[0] == obstacles[i].x && this.y[0] == obstacles[i].y) {
          this.reset();
        }
      }
    } else if (powerTimer > brownPower && powerType == 1) {
      powerTimer = 0;
      powerType = 0;
      brownPower = 0;
      clearInterval(power);
    }

    // Kollar om ormen kraschar in i sig själv
    for (var i = 1; i < snakeLength; i++) {
      if (this.x[0] == this.x[i] && this.y[0] == this.y[i]) {
        this.reset();
      }
    }

    //Kollar om ormen kraschar in i fiende Ormen
    if (points >= 1 && enemy == true) {
      for (var i = 1; i < enemySnake.x.length; i++) {
        if (snake.x[0] == enemySnake.x[i] && snake.y[0] == enemySnake.y[i]) {
          this.reset();
        }
      }
    }
  }

  // Funktion som startar om spelet
  // Nollställer alla variabler
  this.reset = function() {
    if (this != enemySnake) {
      alert("dead, you got " + points + " points");
      clearTimeout(move);
      clearInterval(foodTimer);
      clearInterval(power);
      powerTimer = 0;
      powerType = 0;
      brownPower = 0;
      timer = 0;
      snakeLength = 2;
      this.x = [];
      this.y = [];
      button.innerHTML = "Start"
      dx = 10;
      dy = 0;
      direction = "";
      obstacles = [];
      snake.createSnake();
      obstacle.createObstacle();
      document.getElementById("powerup").innerHTML = "" + 0 + " Sekunder";
      document.getElementById("foodTimer").innerHTML = "" + 10 + " Sekunder";
      document.getElementById("points").innerHTML = "" + points + " Poäng";
      highscores.push(points);
      localStorage.setItem("points", JSON.stringify(highscores));
      highscores.sort();
      document.getElementById('highscore').innerHTML = "1: " + highscores[highscores.length - 1] + "<br />" + "2: " + highscores[highscores.length - 2] + "<br />" + "3: " + highscores[highscores.length - 3] + "<br />" + "4: " + highscores[highscores.length - 4] + "<br />" + "5: " + highscores[highscores.length - 5];


      if (points >= 1 && enemy == true) {
        enemySnake.reset();
      }
      points = 0;
    } else { // Om fiende ormen råkar dör av någon anledning startar den om
      snakeLength = 2;
      enemySnake.x[0] = 20;
      enemySnake.y[0] = 20;
      dx = 10;
      dy = 0;
    }
  }

  // Funktion som gör så att ormen rör på sig
  this.move = function() {
    move = setTimeout(snake.move, speed)
    c.clearRect(0, 0, window.innerWidth, window.innerHeight);

    snake.direction();

    // Gör så att sista ormdelen får näst sista position osv.
    // Lägger sedan till dx och dy på huvudet
    for (var i = 0; i < snakeLength - 1; i++) {
      snake.x[snakeLength - 1 - i] = snake.x[snakeLength - 1 - i - 1];
      snake.y[snakeLength - 1 - i] = snake.y[snakeLength - 1 - i - 1];
    }
    snake.x[0] += dx;
    snake.y[0] += dy;

    // Kör alla nödvändiga funktioner efter ormen rört sig
    // Bland annat rita om allt
    snake.eat();
    snake.dead();
    snake.draw();
    fruit.draw();
    obstacle.draw();
    // Om fiendeläget är på och om den har skapats körs fiende funktionerna också
    if (points > 0 && enemy == true) {
      enemySnake.eat();
      enemySnake.dead();
      enemySnake.draw();
      enemySnake.enemyMove();
    }
  }

  // Funktion som kollar om ormen har ätit en frukt
  this.eat = function() {
    // Kollar vilken typ av frukt den ätit
    for (var i = 0; i < fruit.xfruit.length; i++) {
      if (this.x[0] == fruit.xfruit[i] && this.y[0] == fruit.yfruit[i]) {
        if (fruit.fruitColor[i] == "blue") {

        } else if (fruit.fruitColor[i] == "brown") {
          powerType = 1;
          brownPower += 5;
          powerUpdate();
          clearInterval(power);
          power = setInterval(powerUpdate, 1000)
        }

        // Lägger till poäng och för ormen längre
        snakeLength += 1;
        if (this != enemySnake) {
          points++;
        }

        // Om det är första matbiten och fiendeläge är på, skapa fiende orm
        if (points == 1 && enemy == true && stop == 0) {
          enemySnake = new Snake(10, 0, 10, 2);
          enemySnake.createSnake();
          enemySnake.color = "gray";
          stop = 1;
        }

        // Uppdaterar poäng infoboxen
        // Skapar en ny frukt
        pointsText.innerHTML = "  " + points + " Poäng";
        fruit.createFruit();
        clearInterval(foodTimer);
        timer = 0;
        document.getElementById("foodTimer").innerHTML = "" + 10 - timer + " Sekunder";
        foodTimer = setInterval(foodTimeout, 1000);
      }
      document.getElementById("foodTimer").innerHTML = "" + 10 - timer + " Sekunder";

      // Ökar snabbheten på ormen varannan poäng
      clearTimeout(move);
      move = setTimeout(snake.move, speed - 20 * (points / 2))
    }
  }

  // Funktion för fiendeormen att röra sig
  this.enemyMove = function() {
    // Hittar närmaste axeln till frukten
    // Där rör sig i riktningen som är längst ifrån frukten
    // Kollar också så att den inte kan åka exakt motsatta håll
    var x = Math.abs(enemySnake.x[0] - fruit.xfruit[0]);
    var y = Math.abs(enemySnake.y[0] - fruit.yfruit[0]);
    if (x > y) {
      if (enemySnake.x[0] - fruit.xfruit[0] < 0) {
        if (enemySnake.dx != -10) {
          enemySnake.dx = 10;
          enemySnake.dy = 0;
        }
      } else {
        if (enemySnake.dx != 10) {
          enemySnake.dx = -10;
          enemySnake.dy = 0;
        }
      }
    } else {
      if (enemySnake.y[0] - fruit.yfruit[0] < 0) {
        if (enemySnake.dy != -10) {
          enemySnake.dy = 10;
          enemySnake.dx = 0;
        }
      } else {
        if (enemySnake.dy != 10) {
          enemySnake.dy = -10;
          enemySnake.dx = 0;
        }
      }
    }
    enemySnake.enemyCheck();
    enemyClear = 1;

    // Gör ormen som spelar Ormen
    // Sista får nästa sista osv.
    for (var i = 0; i < snakeLength - 1; i++) {
      enemySnake.x[snakeLength - 1 - i] = enemySnake.x[snakeLength - 1 - i - 1];
      enemySnake.y[snakeLength - 1 - i] = enemySnake.y[snakeLength - 1 - i - 1];
    }
    enemySnake.x[0] += enemySnake.dx;
    enemySnake.y[0] += enemySnake.dy;
  }

  // Funktion för motståndar ormen för att kolla om nästa steg är något fara
  // Om det är det ändras riktnignen
  // Kör funktionen ett par gånger ifall det behövs
  this.enemyCheck = function() {
    var loopAmount = 1;
    while (enemyClear == 1 && loopAmount < 10) {
      enemyClear = 0;

      // Kollar hinder
      for (var i = 0; i < obstacles.length; i++) {
        if (enemySnake.x[0] + enemySnake.dx == obstacles[i].x && enemySnake.y[0] + enemySnake.dy == obstacles[i].y) {
          enemyClear = 1
        }
      }

      // Kollar egen kropp
      for (var i = 1; i < enemySnake.x.length; i++) {
        if (enemySnake.x[0] + enemySnake.dx == enemySnake.x[i] && enemySnake.y[0] + enemySnake.dy == enemySnake.y[i]) {
          enemyClear = 1;
        }
      }

      // Kollar med spelar orm
      for (var i = 0; i < snake.x.length; i++) {
        if (enemySnake.x[0] + enemySnake.dx == snake.x[i] && enemySnake.y[0] + enemySnake.dy == snake.y[i]) {
          enemyClear = 1;
        }
      }

      // Ändrar rikting 90 grader om det behövs
      if (enemyClear == 1) {
        var angle = 90 * Math.PI / 180;
        var test = Math.cos(1.57);
        var cs = 0;
        var sn = Math.sin(angle);
        var x = enemySnake.dx;
        var y = enemySnake.dy;

        enemySnake.dx = x * cs - y * sn;
        enemySnake.dy = x * sn + y * cs;
      }
      loopAmount++;
    }
  }

  // Kollar vilken riktning du tryckte på
  dx = 10;
  this.moveDirection = function(e) {
    var key = e.keyCode;
    if ((key == 68 || key == 39) && dx != -10) {
      //Right
      direction = "right";
    } else if ((key == 87 || key == 38) && dy != 10) {
      //Up
      direction = "up";
    } else if ((key == 83 || key == 40) && dy != -10) {
      //Down
      direction = "down";
    } else if ((key == 65 || key == 37) && dx != 10) {
      //Left
      direction = "left";
    }
  }

  // Funktion som uppdatteras regelbundet och kollar senaste hållet du har tryckt
  // Det gör så att du inte kan trycka två tangenter snabbt och åka in i dig själv i direkt motsatta håll.
  this.direction = function() {
    if (direction == "right" && dx != -10) {
      dx = 10;
      dy = 0;
    } else if (direction == "up" && dy != 10) {
      dx = 0;
      dy = -10;
    } else if (direction == "down" && dy != -10) {
      dx = 0;
      dy = 10;
    } else if (direction == "left" && dx != 10) {
      dx = -10;
      dy = 0;
    }
  }
}

// Frukt Objekt
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
function Fruit(fruitx, fruity) {
  //Deklarerar lokala variabler
  var type = 0;
  var clear = 0;
  var xfruit = [];
  var yfruit = [];
  var fruitColor = [];
  var color = "green"

  this.fruitx = fruitx;
  this.fruity = fruity;
  this.xfruit = xfruit;
  this.yfruit = yfruit;
  this.fruitColor = fruitColor;

  // Rit funktion för frukter
  this.draw = function() {
    c.beginPath();
    c.strokeStyle = "black";
    for (var i = 0; i < xfruit.length; i++) {
      c.fillStyle = fruitColor[i];
      c.strokeRect(xfruit[i], yfruit[i], 10, 10);
      c.fillRect(xfruit[i], yfruit[i], 10, 10);
    }
  }

  // Skapar frukten och slumpar färg
  // Skapar 2 frukter
  this.createFruit = function() {
    var loop = 2;
    for (var j = 0; j < loop; j++) {
      var number = Math.floor(Math.random() * 100);
      if (number > 30) {
        this.color = "green";
      } else if (number <= 10) {
        this.color = "brown";
      }

      // Ser till så att maten inte slumpas på en orm eller på hinder
      while (clear == 0) {
        this.fruitx = 1;
        this.fruity = 1;
        while (this.fruitx % 10 != 0 || this.fruity % 10 != 0) {
          this.fruitx = Math.floor(Math.random() * c.canvas.width);
          this.fruity = Math.floor(Math.random() * c.canvas.height);
        }
        clear = 1;

        // Kollar spelar orm
        for (var i = 0; i < snake.x.length; i++) {
          if (snake.x[i] == this.fruitx && snake.y[i] == this.fruity) {
            clear = 0;
          }
        }

        // Kollar fiende orm
        if (points >= 1 && enemy == true) {
          for (var i = 0; i < enemySnake.x.length; i++) {
            if (enemySnake.x[i] == this.fruitx && enemySnake.y[i] == this.fruity) {
              clear = 0;
            }
          }
        }

        // Kollar hinder
        for (var i = 0; i < obstacles.length; i++) {
          if (obstacles[i].x == this.fruitx && obstacles[i].y == this.fruity) {
            clear = 0;
          }
        }
      }
      clear = 0;

      // Sparar frukten
      xfruit[j] = this.fruitx;
      yfruit[j] = this.fruity;
      fruitColor[j] = this.color;

    }
    fruit.draw();
  }
}

// Hinder objekt
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
var obstacles = [];
var ox = 0;
var oy = 0;

function Obstacle(obstaclex, obstacley) {
  //Deklarerar lokala variable
  var obstacleColor = "black";
  var amount = 5;

  this.x = obstaclex;
  this.y = obstacley
  this.obstacleColor = obstacleColor;

  // Rit funktion för hinder
  // Om en powerup effekt är igång rita ut en annan färg
  this.draw = function() {
    c.beginPath();
    if (powerTimer >= 1 && powerType == 1) {
      this.obstacleColor = "#f2f2f2";
    } else {
      this.obstacleColor = "black";
    }
    c.strokeStyle = "red";
    c.fillStyle = this.obstacleColor;
    for (var i = 0; i < obstacles.length; i++) {
      c.strokeRect(obstacles[i].x, obstacles[i].y, 10, 10);
      c.fillRect(obstacles[i].x, obstacles[i].y, 10, 10);
    }
  }

  // Skapar objekten
  this.createObstacle = function() {
    obstacles = [];
    var clear = 0;

    this.dx = 0;
    this.dy = 0;

    for (var a = 0; a < amount; a++) {
      while (clear == 0) {
        clear = 1;

        var x = Math.floor(Math.random() * c.canvas.width);
        var y = Math.floor(Math.random() * c.canvas.height);
        while (x % 10 != 0 || y % 10 != 0) {
          x = Math.floor(Math.random() * c.canvas.width);
          y = Math.floor(Math.random() * c.canvas.height);
        }

        // Kollar så att en objekt är inte i en annan
        for (var j = 0; j < 5; j++) {
          for (var i = 0; i < obstacles.length; i++) {
            if (obstacles[i].x == x + j * 10 && obstacles[i].y == y) {
              clear = 0;
            }
          }
        }

        // Kollar så att den inte är utanför
        for (var i = 0; i < 5; i++) {
          if (x + i * 10 < 0 || x + i * 10 > c.canvas.width - 10) {
            clear = 0;
          }

          // Kollar så att den inte är nära ormen
          if ((x + i * 10) - snake.x[0] < 50 && y - snake.y[0] < 50) {
            clear = 0;
          }
        }
      }

      // Skapar hindret
      var tile = 0;
      for (var j = 0; j < 5; j++) {
        var obstacle = new Obstacle(x + tile, y);
        obstacles.push(obstacle);
        tile += 10;
      }

      this.draw();
      clear = 0;
    }
  }

  // Funktion för hinder att röra på sig
  this.move = function() {
    //Hittar snabbaste axeln
    obstacleMove = setTimeout(obstacles[0].move, speed * 2)
    for (var i = 0; i < amount; i++) {
      var x = Math.abs(obstacles[i * 5].x - snake.x[0])
      var y = Math.abs(obstacles[i * 5].y - snake.y[0])
      console.log(x);
      console.log(y)
      if (x > y) {
        if (obstacles[i * 5].x - snake.x[0] > 0) {
          ox = -10;
          oy = 0;
        } else {
          ox = 10;
          oy = 0;
        }
      } else {
        if (obstacles[i * 5].y - snake.y[0] > 0) {
          ox = 0;
          oy = -10;
        } else {
          ox = 0;
          oy = 10;
        }
      }
      obstacles[i * 5].obstacleCheck(i * 5);
      for (var k = 0; k < 5; k++) {
        obstacles[i * 5 + k].x += ox;
        obstacles[i * 5 + k].y += oy;
      }
    }
  }

  // Funktion för att kolla om nästa steg kommer krascha in i någon
  this.obstacleCheck = function(a) {
    for (var i = 0; i < 5; i++) {
      for (var j = 0; j < obstacles.length; j++) {
        if (j != a + i) {
          if (obstacles[a + i].x + ox == obstacles[j].x && obstacles[a + i].y + oy == obstacles[j].y) {
            ox = 0;
            oy = 0;
          }
        }
      }
    }
  }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Skapar första sakerna för att starta spelet
var snake = new Snake(0, 0, 10, 2);
var fruit = new Fruit(30, 30);
var obstacle = new Obstacle(0, 0);
snake.createSnake();
obstacle.createObstacle();
fruit.createFruit();

document.addEventListener("keydown", snake.moveDirection);
