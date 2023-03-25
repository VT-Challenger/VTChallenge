$(document).ready(function () {

    //GENERA LAS RONDAS Y LOS PARTIDOS DE LA PRIMERA RONDA DEL TORNEO
    $("#jugadores").change(function () {
        generateRounds($(this).val());
    });
    $("#formulario").submit(function (e) {
        e.preventDefault();
        generateData();
    });
});

function generateRounds(teams) {
    var rounds = $("#rondas");
    var nombresRondas = [
        "Octavos de Final",
        "Cuartos de Final",
        "Semifinal",
        "Final",
    ];
    var numEquipos = teams / 5;

    switch (numEquipos) {
        case 2:
            numRondas = 1;
            break;
        case 4:
            numRondas = 2;
            break;
        case 8:
            numRondas = 3;
            break;
        case 16:
            numRondas = 4;
            break;
        default:
            console.log("Número de equipos no válido");
    }
    rounds.empty();
    // pintar las rondas en la página
    for (var i = 0; i < numRondas; i++) {
        rounds.append(`<div class="row mt-4">
                            <div class="col-12 col-sm-6">
                                <input
                                    class="multisteps-form__input form-control"
                                    type="text"
                                    value="${nombresRondas[i + (4 - numRondas)]
            }"
                                    name="nameRound"
                                    readonly
                                />
                            </div>
                            <div
                                class="col-12 col-sm-6 mt-4 mt-sm-0"
                            >
                                <input
                                    class="multisteps-form__input form-control"
                                    type="date"
                                    name="dateRound"
                                    min=""
                                />
                            </div>
                        </div>`);
    }
    rounds.append(`<div class="button-row d-flex justify-content-between mt-4">
                        <div class="button-borders">
                            <button
                                class="secondary-button secondary-background js-btn-prev"
                                type="button"
                                title="Prev"
                            >
                                Prev
                            </button>
                        </div>
                        <div class="button-borders">
                            <button
                                class="secondary-button secondary-background js-btn-next"
                                type="button"
                                title="Next"
                            >
                                Next
                            </button>
                        </div>
                    </div>`);
    generateClashes(numEquipos, nombresRondas[0 + (4 - numRondas)]);
}

function shuffle(array) {
    var currentIndex = array.length,
        temporaryValue,
        randomIndex;

    // While there remain elements to shuffle...
    while (0 !== currentIndex) {
        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }

    return array;
}

function generateClashes(teams, nombreRonda) {
    var clasification = $("#clasificacion");
    clasification.empty();
    // generar índices de los equipos y mezclarlos
    var indices = [...Array(teams).keys()]; // [0, 1, 2, ..., teams-1]
    shuffle(indices);

    // emparejar índices en parejas para generar enfrentamientos
    var enfrentamientos = [];
    for (var i = 0; i < teams / 2; i++) {
        var equipo1 = indices[i];
        var equipo2 = indices[teams - i - 1];
        enfrentamientos.push([equipo1, equipo2]);
    }
    // pintar los equipos en la página
    var html = `<h1 class="terciary-title-valo mt-4">Partidos de ${nombreRonda}</h1>`;
    for (var i = 0; i < enfrentamientos.length; i++) {
        var equipo1Index = enfrentamientos[i][0];
        var equipo2Index = enfrentamientos[i][1];
        var equipo1 = `Team ${equipo1Index + 1}`;
        var equipo2 = `Team ${equipo2Index + 1}`;
        html += `
      <div class="box-container">
              <div
                  class="box"
              >
                  ${equipo1}
              </div>
              <span class="box-text">VS</span>
              <div
                  class="box"
              >
                  ${equipo2}
              </div>
              <input
                  class="multisteps-form__input form-control"
                  type="time"
                  name="time-match"
                  min=""
              />
      </div>
      `;
    }

    clasification.append(html);
    clasification.append(`<div class="button-row d-flex justify-content-between mt-4">
                            <div class="button-borders">
                                <button
                                    class="secondary-button secondary-background js-btn-prev"
                                    type="button"
                                    title="Prev"
                                >
                                    Prev
                                </button>
                            </div>
                            <div class="button-borders">
                                <button
                                    class="secondary-button secondary-background js-btn-next"
                                    type="button"
                                    title="Next"
                                >
                                    Next
                                </button>
                            </div>
                        </div>`);
}

function generateData() {
    /*DATA JSON TABLA TOURNAMENT */
    var rango = $('select[name="rangoTorneo"]').val();

    var nombreArchivo = rango.substring(
        rango.lastIndexOf("/") + 1,
        rango.lastIndexOf(".")
    );

    var rangoNombre = nombreArchivo
        .substring(0, nombreArchivo.lastIndexOf("_"))
        .replace(/_/g, " ");

    let jsonTournament = {
        Tid: 1,
        Name: $('input[name="nameTorneo"]').val(),
        Rank: rangoNombre,
        DateInit: $('input[name="dateTorneo"]').val(),
        Description: $('textarea[name="descriptionTorneo"]').val(),
        Platform: 1,
        Players: $('select[name="playersTorneo"]').val(),
        Organizator: "uid123182191239",
        Image: $('input[name="imageTournament"]').val(),
    };

    /*DATA JSON TABLA ROUND*/
    var dataRound = document.querySelectorAll("#rondas .row");
    var rounds = [];

    dataRound.forEach((element) => {
        let json = {
            Rid: 1,
            Name: $(element).find("input").val(),
            Date: $(element).find('input[name="dateRound"]').val(),
            Tid: 1,
        };
        rounds.push(json);
    });

    /*DATA JSON TABLA MATCH */
    var dataMatches = document.querySelectorAll(
        "#clasificacion .box-container"
    );

    var matches = [];

    dataMatches.forEach((element) => {
        let json = {
            Mid: 1,
            Tblue: parseInt(
                element
                    .querySelector(".box")
                    .textContent.replace("Team", " ")
                    .trim()
            ),
            TRed: parseInt(
                element
                    .querySelector(".box:nth-of-type(2)")
                    .textContent.replace("Team", " ")
                    .trim()
            ),
            Rblue: 0,
            Rred: 0, // obtener valor del input del segundo equipo
            Date: $(element).find("input").val(),
            Rid: 1,
        };
        matches.push(json);
    });

    if (rounds.length == 0 || matches.length == 0) {
        Swal.fire({
            icon: "error",
            title: "campos vacíos",
            text: "Por favor, rellene todos los campos.",
        });
    } else {
        Swal.fire({
            title: "¿Estas seguro?",
            text: "Revise todos los datos antes de guardar el nuevo torneo.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Si",
            cancelButtonText: "Cancelar",
        }).then((result) => {
            if (result.isConfirmed) {
                showLoading();
                console.log(jsonTournament);
                console.log(rounds);
                console.log(matches);
                $.ajax({
                    url: "",
                    success: (r) => {
                        window.location =
                            "";
                    },
                });
            }
        });
    }
}
function showLoading() {
    document.getElementById("loading").style.display = "block";
}
