function getScoresView(competitionId, drawId, callback) {
	$.ajax({
		method: "GET",
		url: "/api/scores/" + competitionId + (drawId != null ? "/" + drawId : ""),
		success: function(data) {
			callback(data);
		},
		error: function(data){
			callback(data, "Error Loading Scores");
		}
	});
}

function getGameView(gameId, callback){
	$.ajax({
		method: "GET",
		url: "/api/game/" + gameId,
		success: function(data){
			callback(data);
		},
		error: function(data){
			callback(data, "Error Loading Scores");
		}
	});
}

function getDrawView(drawId, callback){
	$.ajax({
		method: "GET",
		url: "/api/draw/" + drawId,
		success: function(data){
			callback(data);
		},
		error: function(data){
			callback(data, "Error Loading Scores");
		}
	});
}