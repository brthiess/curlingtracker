/* Scoreboard */
var fullScreenScoreboardWidth = 719;


function showEvent(eventId, newUrl){
	$('.competition-list-item').removeClass('active');
	$('.competition-list-item[data-id=' + eventId + ']').addClass('active');
	addLoadingToClass('scores-container');
	makeScoresContainerActive();
	getScoresView(eventId, null, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-container-container').html(viewHtml);
			currentCompetitionId = eventId;
			currentDrawId = $('.scores-container-container [data-draw-id]').attr('data-draw-id');
			updateBackButton(true);
			if (typeof newUrl !=="undefined"){
			    history.pushState({action : "showEvent", eventId: eventId}, "", newUrl);
			}
		}
		removeLoadingFromClass('scores-container');
	});
}

function showGame(gameId, eventId, newUrl){
	makeScoresContainerActive();
	addLoadingToClass('scores-container');
	getGameView(gameId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-info-wrapper').html(viewHtml);
			updateBackButton(false, eventId);
		}
		removeLoadingFromClass('scores-container');
		if (typeof newUrl !=="undefined"){
			history.pushState({action : "showGame", gameId: gameId, eventId: eventId}, "", newUrl);
		}
	});
}

function showDraw(drawId, newUrl){
	addLoadingToClass('scores-container');
	getDrawView(drawId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-games-wrapper').html(viewHtml);
		}
		removeLoadingFromClass('scores-container');
		if(typeof newUrl !== "undefined"){
		    history.replaceState({action : "showDraw", drawId: drawId}, "", newUrl);
		}
	})
}

function updateBackButton(isClosing, eventId){
	var onClickAttr = "";
	var backButton = $("[data-back-button]");
	if (isClosing){
		onClickAttr = "closeScores()";
		backButton.attr("data-back-action", "close");
	}
	else {
		onClickAttr = "showEvent('" + eventId + "');";
		backButton.attr("data-back-action", "back");
	}
	backButton.attr("onclick", onClickAttr);
}

function closeScores(){
	makeScoresContainerInActive();
}

function removeLoadingFromClass(className){
	$('.' + className + ' .loading').remove();
	$('.' + className).css('position', '');
}

function addLoadingToClass(className){
	$('.' + className).append('<div class="loading"><div class="spinner"></div></div>');
}
function makeScoresContainerActive(){
	$('.scores-container-container').addClass('active');
	if ($(window).width() < fullScreenScoreboardWidth){
		$('body').css('overflow', 'hidden');
	}
}

function makeScoresContainerInActive(){
	$('.scores-container-container').removeClass('active');
	$('body').css('overflow', '');
}

function showOnScoreboard(section) {
	$("[data-scoreboard-section]").removeClass('active');
	$("[data-scoreboard-section=" + section + "]").addClass('active');
}

function toggleMobileMenu(element) {
	$('.mobile-nav-container').toggleClass('hidden');
	$('body').toggleClass('overflow-hidden');
	toggleHtml(element, 'Close', 'Menu');
}

function toggleHtml(element, text1, text2) {
	if($(element).html() == text1) {
		$(element).html(text2);
	}
	else {
		$(element).html(text1);
	}
}

window.onpopstate = function(event) {
  handlePopState(event.state);
};



$("body").on("click", "[data-pop-history]", function(event){
	history.back();
});

$("body").on("click", "[data-pop-all-history]", function(event){
	while (history.state != null){
		history.back();
	}
});

function handlePopState(state) {
	if (event.state == "showDraw") {
		showDraw(state.drawId);
    }
    else if (event.state == "showEvent") {
		showDraw(state.eventId);
	}
	else if (event.state == "showGame") {
		showGame(state.gameId, state.eventId);
	}
}

/*modal*/
var modalOpen = false;
function showModalWithGameView(gameId, modalTitle) {
	showModal(modalTitle);
	modalLoad(true);
	getGameModalView(gameId, function(html, err){
		modalLoad(false);
		if(err == undefined) {
			$("#modal-content").html(html);
			if(!modalHistoryShowsItsOpen()) {
				history.pushState({modal: "open", title: modalTitle, gameId: gameId}, "", "#score");
			}
		}
		else {
			//error
		}
		
	});	
}
function showModal(modalTitle){
	$("#modal-overlay").addClass("visible");
	$("#modal-title").text(modalTitle);
	$("body").css("overflow", "hidden");
	modalOpen = true;
}
function closeModal(){
	$("#modal-overlay").removeClass("visible");
	$("body").css("overflow", "");
	if(modalHistoryShowsItsOpen()){
		history.back();
	}
	$("#modal-content").html("");
	modalOpen = false;
}
function modalLoad(showLoading){
	if(showLoading){
		$("#modal-content-container").addClass("loading");
	}
	else {
		$("#modal-content-container").removeClass("loading");
	}
}
//return true if the current history state suggests that the modal is open
function modalHistoryShowsItsOpen(){
	if(history.state !== null && history.state.modal == 'open') {
		return true;
	}
	else {
		return false;
	}
}
window.onpopstate = function(event) {
	if(modalOpen == true){
		closeModal();
	}
	console.log(history.state);
	if(modalOpen == false && modalHistoryShowsItsOpen()){
		showModalWithGameView(history.state.gameId, history.state.modalTitle);
	}

}


function updateDrawId(competitionId, drawId){
	$('.scores-info-container').addClass('loading');
	getScoresGamesView(competitionId, drawId, function(viewHtml){
		$('.scores-games-wrapper').replaceWith(viewHtml);
		currentCompetitionId = competitionId;
		currentDrawId = $('.scores-container [data-draw-id]').attr('data-draw-id');
		$('.scores-info-container').removeClass('loading');
	});
}

