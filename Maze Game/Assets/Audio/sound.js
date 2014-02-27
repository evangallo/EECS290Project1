@script RequireComponent(AudioSource)
 var fireRate : float = 0.14;
private var nextFire : float = 0.0;
var front : AudioClip;

var back: AudioClip;

function Update(){
 
Fire();
 
}

function Fire(){

if (Input.GetButton("Fire1") && Time.time>nextFire){
	nextFire = Time.time + fireRate;
	audio.Play();
//	ForceFire();

}


}