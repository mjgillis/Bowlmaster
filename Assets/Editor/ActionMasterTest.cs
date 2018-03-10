using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster actionMaster;

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup () {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));    
    }

    [Test]
    public void T02Bowl8ReturnsTidy() 
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28ReturnsEndTurn()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04BowlStrikesAllFrames()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //1
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //2
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //3
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //4
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //5
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //6
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //7
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //8
        Assert.AreEqual(endTurn, actionMaster.Bowl(10)); //9
        Assert.AreEqual(reset, actionMaster.Bowl(10)); //10(A)
        Assert.AreEqual(reset, actionMaster.Bowl(10)); //10(B)
        Assert.AreEqual(endGame, actionMaster.Bowl(10)); //10(C)
    }

    [Test]
    public void T05BowlSparesAllFrames()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //1A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //1B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //2A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //2B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //3A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //3B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //4A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //4B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //5A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //5B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //6A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //6B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //7A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //7B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //8A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //8B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //9A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //9B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //10(A)
        Assert.AreEqual(reset, actionMaster.Bowl(5)); //10(B)
        Assert.AreEqual(endGame, actionMaster.Bowl(10)); //10(C)
    }

    [Test]
    public void T06No3rdRollIn10thFrame()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //1A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //1B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //2A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //2B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //3A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //3B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //4A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //4B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //5A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //5B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //6A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //6B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //7A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //7B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //8A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //8B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //9A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //9B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //10(A)
        Assert.AreEqual(endGame, actionMaster.Bowl(2)); //10(B)

    }

    [Test]
    public void T0710thFrameStrike5PinsNoTidy() 
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //1A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //1B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //2A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //2B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //3A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //3B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //4A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //4B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //5A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //5B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //6A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //6B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //7A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //7B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //8A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //8B

        Assert.AreEqual(tidy, actionMaster.Bowl(5)); //9A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //9B

        Assert.AreEqual(reset, actionMaster.Bowl(10)); //10(A)
        Assert.AreEqual(tidy, actionMaster.Bowl(2)); //10(B)
        Assert.AreEqual(endGame, actionMaster.Bowl(2)); //10(C)
    }

    [Test]
    public void Test08ZeroPlus10Spare() 
    {
        int[] rolls = { 0, 10, 5 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void Test09Dondi10thFrameTurkey()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10)); Assert.AreEqual(reset, actionMaster.Bowl(10)); Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void Test10ZeroandOne()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0)); //1A
        Assert.AreEqual(endTurn, actionMaster.Bowl(5)); //1B
    }
}