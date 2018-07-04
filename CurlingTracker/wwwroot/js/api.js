function getScoresView(competitionId, drawId, callback) {
	setTimeout(function(){$.ajax({
		method: "GET",
		url: "/api/scores/" + competitionId + (drawId != null ? "/" + drawId : ""),
		success: function(data) {
			callback(data);
		},
		error: function(data){
			console.log(data);
		}
	})}, 1000);
}

function getGameView(gameId, callback){
	setTimeout(function(){$.ajax({
		method: "GET",
		url: "/api/game/" + gameId,
		success: function(data){
			callback(data);
		},
		error: function(data){
			console.log(data);
		}
	})}, 1000);
}