function getDrawScoresJSON(competitionId, drawId, callback, error){
	$.ajax({
		url: '/data/scores/' + competitionId + '/' + drawId,
		dataType: 'json',
		success: function(data){
			callback(data);
		},
		error: function(data){
			callback(data, "Error Loading Scores");
		}
	});
}