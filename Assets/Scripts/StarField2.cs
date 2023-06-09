﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//from https://mrlovelies.medium.com/unity-dev-stars-ce18d43dd55# by Alex Somerville - Made some modifications on my own =) 

public class StarField2 : MonoBehaviour
{
	public int MaxStars = 100;
	public float StarSize = 0.1f;
	public float StarSizeRange = 0.5f;
	public float FieldWidth = 20f;
	public float FieldHeight = 25f;
	public bool Colorize = false;
	public float speed = 0.5f;

	float xOffset;
	float yOffset;

	private Transform bgCamera;

	ParticleSystem Particles;
	ParticleSystem.Particle[] Stars;

	void Awake()
	{
		bgCamera = GameObject.FindWithTag("MainCamera").transform; // "BG_Camera").transform;
		//bgCamera = Camera.main.transform;
		Stars = new ParticleSystem.Particle[MaxStars];
		Particles = GetComponent<ParticleSystem>();

		Assert.IsNotNull(Particles, "Particle system missing from object!");
		Assert.IsNotNull(bgCamera, "BG Camera missing from object!");

		xOffset = FieldWidth * 0.5f;                                                                                                        // Offset the coordinates to distribute the spread
		yOffset = FieldHeight * 0.5f;                                                                                                       // around the object's center

		for (int i=0; i<MaxStars; i++)
        {
			float randSize = Random.Range(StarSizeRange, StarSizeRange + 1f);
			float scaledColor = (true == Colorize) ? randSize + StarSizeRange : 1f;
			Stars[i].position = GetRandomInRectangle(FieldWidth, FieldHeight) + transform.position;
			Stars[i].startSize = StarSize * randSize;
			Stars[i].startColor = (Color32)new Color(1f, scaledColor, scaledColor, 1f);
        }
		Particles.SetParticles(Stars, Stars.Length);
	}

	Vector3 GetRandomInRectangle(float width, float height)
    {
		float x = Random.Range(0, width);
		float y = Random.Range(0, height);
		return new Vector3(x - xOffset, y - yOffset, 0);
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		CalculateMovement();
    }

	void CalculateMovement()
    {
		transform.position = new Vector3(transform.position.x - speed * Time.deltaTime,
			transform.position.y,
			transform.position.z);
		for(int i=0;i<MaxStars;i++)
        {
			Vector3 pos = Stars[i].position + transform.position;

			if(pos.x < (bgCamera.position.x - xOffset))
            {
				pos.x += FieldWidth;
            } else
			if (pos.x > (bgCamera.position.x + xOffset))
			{
				pos.x -= FieldWidth;
			};

			if (pos.y < (bgCamera.position.y - yOffset))
			{
				pos.y += FieldHeight;
			}
			else
			if (pos.y > (bgCamera.position.y + yOffset))
			{
				pos.y -= FieldHeight;
			};

			Stars[i].position = pos - transform.position;
        }
		Particles.SetParticles(Stars, Stars.Length);
    }
}//class



	