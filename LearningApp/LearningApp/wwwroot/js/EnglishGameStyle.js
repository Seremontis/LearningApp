var counter = 0;
function test(value) {
    let items = document.querySelectorAll(".item");

    for (var i = 0; i < value.length; i++) {
        let taghide = document.createElement('input');
        taghide.type = "hidden"
        taghide.value = value[i].id;
        let taghide2 = taghide.cloneNode();
        let word = value[i];
        let freeIndex = GetIndex(items);
        items[freeIndex].textContent = word.englishMeaning;
        items[freeIndex].appendChild(taghide);

        freeIndex = GetIndex(items);
        items[freeIndex].textContent = word.polishMeaning;
        items[freeIndex].appendChild(taghide2);
    }
}
function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

function GetIndex(items) {
    let index = 0;
    do {
        index = getRandomInt(0, items.length)
    } while (items[index].textContent != '');
    return index;
}

function select(value) {
    if (counter==1) {
        value.classList.add("select");
        counter++;
    }
    if (counter == 2) {
        let selected = value.parentElement.querySelectorAll(".select");
        let s1 = selected[0].querySelector("input").value;
        let s2 = selected[1].querySelector("input").value;

        if (s1 === s2) {
            selected.forEach(x => {
                x.classList.remove("select");
                x.classList.add("done");
            });
            counter = 0;
        }
        else {
            counter++;
        }
    }
    else {
        let selected = value.parentElement.querySelectorAll(".select");
        selected.forEach(x => x.classList.remove("select"));

        value.classList.add("select");
        counter = 1;
    }
}