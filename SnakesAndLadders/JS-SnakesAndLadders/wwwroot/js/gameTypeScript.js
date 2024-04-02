var tsgame;
(function (tsgame) {
    const msg = document.querySelector("#msg");
    const redPawn = document.querySelector("#p1");
    const bluePawn = document.querySelector("#p2");
    const btnStart = document.querySelector("#btnStart");
    const btnDice = document.querySelector("#btnDice");
    const btnStep = document.querySelector("#btnStep");
    const allSqures = [];
    const snakeSpots = [];
    const ladderSpots = [];
    let currentPawn = redPawn;
    let stepsForCurrentTurn = 0;
    let targetIndex = 0;
    let diceSound = new Audio("/audio/dice.mp3");
    let snakeSound = new Audio("/audio/snake.mp3");
    let ladderSound = new Audio("/audio/ladder.mp3");
    let winnerSound = new Audio("/audio/winner.mp3");
    let startSound = new Audio("/audio/start.mp3");
    allSqures.push(...document.querySelectorAll(".squres"));
    allSqures.sort((a, b) => a.id - b.id);
    snakeSpots.push(...document.querySelectorAll(".sn"));
    ladderSpots.push(...document.querySelectorAll(".la"));
    btnStart.addEventListener("click", startGame);
    btnDice.addEventListener("click", throwTheDice);
    btnStep.addEventListener("click", takeSteps);
    function takeSteps() {
        let e = currentPawn.parentElement;
        let nextIndex = getCurrentIndex() + 1;
        let nextSqure = allSqures[nextIndex];
        checkWinner(nextIndex);
        e.removeChild(currentPawn);
        nextSqure.appendChild(currentPawn);
        if (nextIndex === targetIndex) {
            checkSnakes();
            checkLadders();
            changeButtonsState();
            currentPawn = currentPawn == redPawn ? bluePawn : redPawn;
            showMessage(false);
        }
    }
    function throwTheDice() {
        stepsForCurrentTurn = getRandomInt(1, 7);
        targetIndex = getCurrentIndex() + stepsForCurrentTurn;
        diceSound.play();
        msg.textContent = `Take ${stepsForCurrentTurn} steps!`;
        changeButtonsState();
    }
    function startGame() {
        startSound.play();
        if (getCurrentIndex() != 0) {
            location.reload();
        }
        currentPawn = redPawn;
        showMessage(false);
        btnDice.disabled = false;
        btnStep.disabled = true;
    }
    function checkWinner(nextIndex) {
        if (nextIndex === 99) {
            winnerSound.play();
            let winner = true;
            showMessage(winner);
            msg.classList.remove("bg-info-subtle");
            let c = currentPawn == redPawn ? "bg-danger" : "bg-secondary";
            msg.classList.add(c);
            btnDice.disabled = true;
            btnStep.disabled = true;
        }
    }
    function checkIt(snakeOrLadder, alertContent) {
        let targetSqure = allSqures[targetIndex];
        let list = snakeOrLadder == "snake" ? snakeSpots : ladderSpots;
        let spot = list.find(element => element.id === targetSqure.id);
        if (spot) {
            alert(alertContent);
            let i;
            let n;
            let prefix = snakeOrLadder == "snake" ? "sn" : "la";
            for (i = 1; i <= 10; i++) {
                let isTrue = spot.classList.contains(`${prefix}${i}`);
                if (isTrue) {
                    n = i;
                }
                ;
            }
            let newIndex;
            if (snakeOrLadder == "snake") {
                newIndex = allSqures.findIndex((element) => element.classList.contains(`sn${n}`));
                snakeSound.play();
            }
            else {
                let lst = allSqures.filter(s => s.classList.contains(`la${n}`));
                let sq = lst[1];
                newIndex = allSqures.indexOf(sq);
                ladderSound.play();
            }
            currentPawn.parentElement.removeChild(currentPawn);
            allSqures[newIndex].appendChild(currentPawn);
        }
    }
    function checkSnakes() {
        checkIt("snake", "OUCH!!! A Snake!!!\nClick OK to slide down!");
    }
    function checkLadders() {
        checkIt("ladder", "HOORAY!!! A Ladder!!!\nClick OK to climb up!");
    }
    function getCurrentIndex() {
        let e = currentPawn.parentElement;
        return allSqures.findIndex((element) => element.id === e.id);
    }
    function changeButtonsState() {
        btnStep.disabled = btnStep.disabled == true ? false : true;
        btnDice.disabled = btnDice.disabled == true ? false : true;
    }
    function showMessage(winner) {
        let c = currentPawn == redPawn ? "Red" : "Blue";
        let content = winner ? `WOW! ${c} is the Winner` : `Current Turn: ${c} – Throw the dice!`;
        msg.textContent = content;
    }
    function getRandomInt(min = 0, max = 0) {
        const minCeiled = Math.ceil(min);
        const maxFloored = Math.floor(max);
        return Math.floor(Math.random() * (maxFloored - minCeiled) + minCeiled);
    }
})(tsgame || (tsgame = {}));
//# sourceMappingURL=gameTypeScript.js.map