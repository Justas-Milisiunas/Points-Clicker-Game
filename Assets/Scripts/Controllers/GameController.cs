using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PointCreator pointCreator;
    public RopeCreator ropeCreator;
    public TextAsset levelsData;
    public LevelSelectUIController levelsSelectUI;

    private LevelLoader levelLoader;
    private Level currentLevel;

    private List<PointController> createdPoints;
    private int lasClickedPointOrder = 0;

    private List<RopeController> createdRopes;

    private Queue<RopeAnimation> ropesAnimationQueue;
    private bool ropeAnimationIsActive = false;
    private bool levelFinished = false;

    void Start()
    {
        createdPoints = new List<PointController>();
        createdRopes = new List<RopeController>();

        ropesAnimationQueue = new Queue<RopeAnimation>();

        levelLoader = new LevelLoader(levelsData);

        levelsSelectUI.LevelSelected += HandleLevelSelected;

        AddOptionsToLevelSelectorDropdown();
    }

    private void HandleLevelSelected(int levelIndex)
    {
        var selectedLevel = levelLoader.LoadLevel(levelIndex - 1);
        levelsSelectUI.Hide();

        DestroyPointsAndRopes();
        CreateLevelPoins(selectedLevel);

        levelFinished = false;
    }

    private void AddOptionsToLevelSelectorDropdown()
    {
        int numberOfLevels = levelLoader.GetNumberOfLevels();
        var options = new List<string>();
        options.Add("-");

        for (int i = 0; i < numberOfLevels; i++)
        {
            options.Add($"Level {i + 1}");
        }

        levelsSelectUI.AddDropdownOptions(options);
    }

    private void CreateLevelPoins(Level level)
    {
        level.Points.ForEach(point =>
        {
            var createdPoint = pointCreator.CreatePoint(point);
            createdPoint.OnClick += PointClicked;

            createdPoints.Add(createdPoint);
        });
    }

    private void PointClicked(PointController clickedPoint)
    {
        if (clickedPoint.Order == lasClickedPointOrder + 1)
        {
            clickedPoint.StartClickAnimation();
            lasClickedPointOrder = clickedPoint.Order;

            // If not first point draws a line
            if (lasClickedPointOrder != 1)
            {
                var lastPoint = createdPoints[lasClickedPointOrder - 2];
                DrawRope(lastPoint.transform, clickedPoint.transform);
            }

            // If last point connects to first
            if (lasClickedPointOrder == createdPoints.Count)
            {
                var firstPoint = createdPoints.First(p => p.Order == 1);
                DrawRope(clickedPoint.transform, firstPoint.transform);

                levelFinished = true;
            }
        }
    }

    private void DrawRope(Transform start, Transform end)
    {
        // If there is rope animation currently active, queue up the enxt trope animation
        if (ropeAnimationIsActive)
        {
            ropesAnimationQueue.Enqueue(new RopeAnimation(start, end));
            return;
        }

        var createdRope = ropeCreator.CreateRope(start, end);
        createdRope.ExtendingFinished += HandleRopeExtendingFinished;
        createdRopes.Add(createdRope);

        ropeAnimationIsActive = true;
    }

    private void HandleRopeExtendingFinished()
    {
        ropeAnimationIsActive = false;

        if (ropesAnimationQueue.Count > 0)
        {
            var nextRopeAnimation = ropesAnimationQueue.Dequeue();
            DrawRope(nextRopeAnimation.Start, nextRopeAnimation.End);
        }
        else if (ropesAnimationQueue.Count == 0 && levelFinished)
        {
            Invoke(nameof(WaitWhenLevelFinished), 1f);
        }
    }

    private void WaitWhenLevelFinished()
    {
        DestroyPointsAndRopes();
        levelsSelectUI.Show();
        lasClickedPointOrder = 0;
    }

    private void DestroyPointsAndRopes()
    {
        createdPoints.ForEach(p => Destroy(p.gameObject));
        createdPoints.Clear();

        createdRopes.ForEach(r => Destroy(r.gameObject));
        createdRopes.Clear();
    }

    private class RopeAnimation
    {
        public Transform Start;
        public Transform End;

        public RopeAnimation(Transform start, Transform end)
        {
            Start = start;
            End = end;
        }
    }
}
