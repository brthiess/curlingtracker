/* Scoreboard */
var fullScreenScoreboardWidth = 719;
const originalPath = window.location.pathname;

function showEvent(eventId, drawId, newMobileUrl){
	$('.competition-list-item').removeClass('active');
	var eventObject = $('.competition-list-item[data-id=' + eventId + ']');
	eventObject.addClass('active');
	addLoadingToClass('scores-container');
	makeScoresContainerActive();
	var eventName = eventObject.attr("data-event-name");
	SetScoreboardTitle(eventName);
	getScoresView(eventId, drawId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-container-container').html(viewHtml);
			currentCompetitionId = eventId;
			currentDrawId = $('.scores-container-container [data-draw-id]').attr('data-draw-id');
			updateBackButton(true);
			if (typeof newMobileUrl !=="undefined"){
			    history.pushState({action : "showEvent", eventId: eventId}, "", newMobileUrl);
			}
		}
		removeLoadingFromClass('scores-container');
	});
}

function showGame(gameId, eventId, newMobileUrl){
	makeScoresContainerActive();
	addLoadingToClass('scores-container');
	getGameView(gameId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-info-wrapper').html(viewHtml);
			updateBackButton(false, eventId);
		}
		removeLoadingFromClass('scores-container');
		if (typeof newMobileUrl !=="undefined"){
			history.pushState({action : "showGame", gameId: gameId, eventId: eventId}, "", newMobileUrl);
		}
	});
}

function showDraw(eventId, drawId, newMobileUrl){
	addLoadingToClass('scores-container');
	getDrawView(drawId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-games-wrapper').html(viewHtml);
		}
		removeLoadingFromClass('scores-container');
		if(typeof newMobileUrl !== "undefined"){
		    history.replaceState({action : "showDraw", drawId: drawId, eventId: eventId}, "", newMobileUrl);
		}
	})
}

function showOnScoreboard(section) {
	$("[data-scoreboard-section]").removeClass('active');
	$("[data-scoreboard-section=" + section + "]").addClass('active');
	if (section == 'standings'){
		updateBracketScrollListeners();
	}
}


function showBracket(bracketId){
	$("[data-bracket-id]").removeClass("active");
	$("[data-bracket-id='" + bracketId + "']").addClass("active");
}

function updateBackButton(isClosing, eventId){
	var backButton = $("[data-back-button]");
	if (isClosing){
		backButton.attr("data-back-action", "close");
	}
	else {
		backButton.attr("data-back-action", "back");
	}
}

function updateBracket(bracketLevel){
	if (bracketLevel.checked == true){
		showBracket($(bracketLevel).attr("data-bracket-id"));
	}
	updateBracketScrollListeners();
}

/* bracket arrow/scroll functions */
function updateBracketScrollListeners(){
	handleBracketScroll();
	document.querySelector('.bracket-container').removeEventListener('scroll', handleBracketScroll)
	document.querySelector('.bracket-container.active').addEventListener('scroll', handleBracketScroll);
}

function handleBracketScroll(){
	obj = document.querySelector('.bracket-container.active');
	if( obj.scrollLeft === (obj.scrollWidth - obj.offsetWidth))
	{
		showLeftBracketArrow();
		hideRightBracketArrow();
	}
	else if (obj.scrollLeft <= 1) {
		showRightBracketArrow();
		hideLeftBracketArrow();
	}
}
function scrollBracketLeft(){
	scrollBracket(-800);
}

function scrollBracketRight(){
	scrollBracket(800);
}

function showRightBracketArrow(){
	$(".brackets-container-arrow-right").addClass("active");
}
function showLeftBracketArrow(){
	$(".brackets-container-arrow-left").addClass("active");
}
function hideLeftBracketArrow(){
	$(".brackets-container-arrow-left").removeClass("active");
}
function hideRightBracketArrow(){
	$(".brackets-container-arrow-right").removeClass("active");
}

function scrollBracket(amount){
	document.querySelector('.bracket-container.active').scroll({
		top: 0, 
		left: amount, 
		behavior: 'smooth' 
	});
}
/* end bracket scroll/arrow functions */ 

function closeScores(newUrl){
	makeScoresContainerInActive();
	if (typeof(newUrl) !== "undefined"){
		history.pushState({action: "closeScores"}, "", newUrl);
	}
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

function SetScoreboardTitle(title) {
	$(".scores-top-header-title").html(title);
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
	if (history.state != null){
		history.back();
	}
});

function handlePopState(state) {
	if (typeof(state) === "undefined" || state == null || state.action == "closeScores"){
		closeScores();
	}
	else if (state.action == "showDraw") {
		showEvent(state.eventId, state.drawId);
    }
    else if (state.action == "showEvent") {
		showEvent(state.eventId);
	}
	else if (state.action == "showGame") {
		showGame(state.gameId, state.eventId);
	}
}

