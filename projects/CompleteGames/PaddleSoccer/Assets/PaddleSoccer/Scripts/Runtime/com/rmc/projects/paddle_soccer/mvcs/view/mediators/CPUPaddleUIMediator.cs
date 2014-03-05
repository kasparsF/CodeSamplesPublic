/**
 * Copyright (C) 2005-2014 by Rivello Multimedia Consulting (RMC).                    
 * code [at] RivelloMultimediaConsulting [dot] com                                                  
 *                                                                      
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the      
 * "Software"), to deal in the Software without restriction, including  
 * without limitation the rights to use, copy, modify, merge, publish,  
 * distribute, sublicense, and#or sell copies of the Software, and to   
 * permit persons to whom the Software is furn
 * 
 * ished to do so, subject to
 * the following conditions:                                            
 *                                                                      
 * The above copyright notice and this permission notice shall be       
 * included in all copies or substantial portions of the Software.      
 *                                                                      
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.                                      
 */
// Marks the right margin of code *******************************************************************

//--------------------------------------
//  Imports
//--------------------------------------
using com.rmc.projects.paddle_soccer.mvcs.view.ui;
using com.rmc.projects.paddle_soccer.mvcs.controller.signals;
using com.rmc.projects.paddle_soccer.mvcs.model;
using com.rmc.projects.animation_monitor;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.paddle_soccer.mvcs.view.mediators
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	public class CPUPaddleUIMediator : SuperPaddleUIMediator
	{
		
		//--------------------------------------
		//  Properties
		//--------------------------------------

		/*
		 * NOTE: According to my tests and
		 * 		http://kendlj.wordpress.com/2014/01/03/unit-testing-strangeioc-mediator/
		 * 
		 * 		We cannot do "mediationBinder.Bind<IControllerUI>().To<VirtualControllerUIMediator>();
		 * 
		 * 		So this is a workaround
		 * 
		 **/
		[Inject]
		public CPUPaddleUI viewConcrete 	
		{ 
			set {
				view = value;
			}
		}

		// PUBLIC

		
		// PUBLIC STATIC
		
		// PRIVATE
		
		// PRIVATE STATIC
		/// <summary>
		/// Damage
		/// </summary>
		private const int _DAMAGE_GIVEN_PER_HIT = 10;


		
		//--------------------------------------
		//  Methods
		//--------------------------------------
		/// <summary>
		/// Raises the register event.
		/// </summary>
		public override void OnRegister()
		{
			base.OnRegister();
			//


		}

		/// <summary>
		/// Raises the remove event.
		/// </summary>
		public override void OnRemove()
		{
			base.OnRemove();
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void Start ()
		{

		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		public void Update()
		{

			//*************************************************
			//******* CORE LOGIC                     **********
			//*************************************************
			if (iGameModel.gameState == GameState.ROUND_DURING_CORE_GAMEPLAY) {

				//view.doMoveToTarget();

			}
			//*************************************************
			//*************************************************
			//*************************************************
			


		}
		
		//	PUBLIC
		
		
		// PRIVATE



		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events
		//--------------------------------------
		/// <summary>
		/// When the game state changed signal.
		/// </summary>
		/// <param name="aGameState">A game state.</param>
		override protected void _onGameStateChangedSignal (GameState aGameState)
		{
			//
			base._onGameStateChangedSignal(aGameState);

			//todo:change to
			//if (aGameState == GameState.ROUND_DURING_CORE_GAMEPLAY) {
			if (aGameState == GameState.ROUND_DURING_CORE_GAMEPLAY) {
				view.isRunningUpdate = true;
				view.doTweenToStartingPosition(0);
			} else if (aGameState == GameState.ROUND_START) {
				view.isRunningUpdate = false;
			} else if (aGameState == GameState.GAME_START) {
				view.doTweenToOffscreenPosition(1f);
				
			}
			
		}



		/// <summary>
		/// When the user interface animation complete signal.
		/// </summary>
		/// <param name="aUIAnimationMonitorEventVO">A user interface animation monitor event V.</param>
		override protected void _onUIAnimationCompleteSignal (AnimationMonitorEventVO aUIAnimationMonitorEventVO )
		{
			//
			base._onUIAnimationCompleteSignal(aUIAnimationMonitorEventVO);

			/*
			//MATCH CASE OF STRING
			string animationClipNameUpper_string = view.animationType.ToString();


			//WE MOSTLY CARE ABOUT WHEN THE ANIMATION IS OVER *INCLUDING* ANY COSMETIC DELAYS WE ADDED
			if (aUIAnimationMonitorEventVO.animationMonitorEventType == AnimationMonitorEventType.POST_COMPLETE) {

				if (iGameModel.gameState == GameState.ROUND_DURING_CORE_GAMEPLAY) {

					if (animationClipNameUpper_string == AnimationType.JUMP.ToString()) {

						view.doPlayAnimation (AnimationType.WALK, 0, 0);

					} else if (animationClipNameUpper_string == AnimationType.DIE.ToString()) {

						//DESTROY OBJECT, UPDATE SCORE
						enemyDiedSignal.Dispatch (view);

						//PLAY SOUND
						soundPlaySignal.Dispatch (new SoundPlayVO (SoundType.ENEMY_DIE));

					} else if (animationClipNameUpper_string == AnimationType.WALK.ToString()) {

						//PLAY SOUND
						soundPlaySignal.Dispatch (new SoundPlayVO (SoundType.ENEMY_FOOSTEP));

					} else if (animationClipNameUpper_string == AnimationType.TAKE_HIT.ToString()) {

						//
						view.doPlayAnimation (AnimationType.WALK, 0, 0);

					} else if (animationClipNameUpper_string == AnimationType.ATTACK.ToString()) {
						
						//TODO, INFLICT DAMAGE LESS, ONLY WHEN ANIMATION 'LOOPS'
						_doInflictDamage();

						//PLAY SOUND
						soundPlaySignal.Dispatch (new SoundPlayVO (SoundType.ENEMY_ATTACK));

						//
						view.doPlayAnimation (AnimationType.ATTACK, 0, 0);
					}
				} else {
					 
					//
					view.doStopAnimation();
				}

			} else if (aUIAnimationMonitorEventVO.animationMonitorEventType == AnimationMonitorEventType.COMPLETE) {

				//BUT SOMETIMES WE JUST WANT TO TRIGGER A SOUND

				if (animationClipNameUpper_string == AnimationType.JUMP.ToString()) {

					//PLAY SOUND
					soundPlaySignal.Dispatch (new SoundPlayVO (SoundType.ENEMY_FOOSTEP));
					
				} 
			}
			*/
		}
	}
}

