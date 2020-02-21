// create an array with nodes
//var nodes = new vis.DataSet([
//    { id: 1, label: 'Node 1' },
//    { id: 2, label: 'Node 2' },
//    { id: 3, label: 'Node 3' },
//    { id: 4, label: 'Node 4' },
//    { id: 5, label: 'Node 5' }
//]);
(function () {
    var programa = "";
    $("#Boton").on("click", function () {
        programa = $("#ProgramaInput").val();
        //console.log(`progra ${programa}`);

        if (programa == "") {
            programa = "400";
        }
        console.log(`progra ${programa}`);

 
            //burbujas    
            $.ajax({
                type: "POST",
                async: false,

                url: 'ProgramasPrestacions/Burbujas',
                data: { programa },

                success: function (msg) {
                    Burbujas = msg;
                    console.log("Anduvo ok", programa, msg[0].GrupoPrograma)
                    console.log(JSON.stringify(msg));
                
                },
                error: function (msg) {
                    console.log(msg)
                }
            });
           



            // create an array with edges
            //var edges = new vis.DataSet([
            //    { from: 1, to: 3 },
            //    { from: 1, to: 2 },
            //    { from: 2, to: 4 },
            //    { from: 2, to: 5 }
            //]);

            //flechas
                $.ajax({
                    type: "POST",
                    async: false,

                    url: 'ProgramasPrestacions/Flechas',
                    data: { programa },
                    success: function (msg) {
                        flechas = msg;
                        console.log("Anduvo ok", programa, msg[0].GrupoPrograma)
                        console.log(JSON.stringify(msg));
                    
                    },
                    error: function (msg) {
                        console.log(msg)
                    }
        });


        var edges = new vis.DataSet(flechas);//flechas
        var nodes = new vis.DataSet(Burbujas );//burbujas


            // create a network
            var container = document.getElementById('mynetwork');

            // provide the data in the vis format
            var data = {
                nodes: nodes,
                edges: edges
            };
        var options = {
            layout: {
                randomSeed: 120,
                improvedLayout: true,
                hierarchical: {
                    enabled: false,
                    levelSeparation: 250,
                    nodeSpacing: 5,
                    treeSpacing: 200,
                    blockShifting: true,
                    edgeMinimization: true,
                    parentCentralization: true,
                    direction: 'RL',        // UD, DU, LR, RL
                    sortMethod: 'hubsize'   // hubsize, directed
                }
            },
            edges: {
                arrows: {
                    to: { enabled: true, scaleFactor: 1, type: 'arrow' },
                    middle: { enabled: false, scaleFactor: 1, type: 'arrow' },
                    from: { enabled: true, scaleFactor: 25, type: 'arrow' }
                },
                arrowStrikethrough: true,
                physics: false,
                font: {
                    color: '#343434',
                    size: 8, // px
                    face: 'arial',
                    background: 'none',
                    //strokeWidth: 5, // px
                    //strokeColor: '#f44ff',
                    align: 'horizontal',
                    multi: false,
                },
                scaling: {
                    min: 1,
                    max: 5,
                    label: {
                        enabled: true,
                        min: 1,
                        max: 30,
                        maxVisible: 50,
                        drawThreshold: 8
                    },
                }
            }
        }

            // initialize your network!
                var network = new vis.Network(container, data, options);
      });
})()