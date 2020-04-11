using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //[SerializeField] private GameObject _startTile;
    //[SerializeField] private GameObject _endTile;
    //[SerializeField] private GameObject _straightTile;
    //[SerializeField] private GameObject _curveTile;

    //[SerializeField] private int _tileCount;

    //private Vector2 _position = Vector2.zero;

    //private int _offset = 5;
    //void Start()
    //{
    //    CalculatePositions();
    //}

    //private void CalculatePositions()
    //{
    //    Vector2 previousPosition;
    //    int randomTileRotation, previousTileRotation, randomTileNumber;

    //    //Start tile
    //    randomTileRotation = UnityEngine.Random.Range(0, 4);
    //    //second tile
    //    randomTileNumber = UnityEngine.Random.Range(0, 2);
    //    Debug.Log("spawnNummer: " + randomTileRotation);
    //    switch (randomTileRotation)
    //    {
    //        case 0:
    //            PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 0));
    //            if (randomTileNumber == 0)
    //            {
    //                PlaceTile(_straightTile, _position + new Vector2(0, _offset), Quaternion.Euler(0, 0, 0));
    //            }
    //            else
    //            {
    //                PlaceTile(_curveTile, _position + new Vector2(0, _offset), Quaternion.Euler(0, 0, 0));
    //            }              
    //            previousPosition = _position + new Vector2(0, _offset);
    //            break;
    //        case 1:
    //            PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, -90));
    //            if (randomTileNumber == 0)
    //            {
    //                PlaceTile(_straightTile, _position + new Vector2(_offset, 0), Quaternion.Euler(0, 0, 90));
    //            }
    //            else
    //            {
    //                PlaceTile(_curveTile, _position + new Vector2(_offset, 0), Quaternion.Euler(0, 0, 90));
    //            }               
    //            previousPosition = _position + new Vector2(_offset, 0);
    //            break;
    //        case 2:
    //            PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 180));
    //            if (randomTileNumber == 0)
    //            {
    //                PlaceTile(_straightTile, _position + new Vector2(0, -_offset), Quaternion.Euler(0, 0, 0));
    //            }
    //            else
    //            {
    //                PlaceTile(_curveTile, _position + new Vector2(0, -_offset), Quaternion.Euler(0, 0, 0));
    //            }               
    //            previousPosition = _position + new Vector2(0, -_offset);
    //            break;
    //        case 3:
    //            PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 90));
    //            if (randomTileNumber == 0)
    //            {
    //                PlaceTile(_straightTile, _position + new Vector2(-_offset, 0), Quaternion.Euler(0, 0, 90));
    //            }
    //            else
    //            {
    //                PlaceTile(_curveTile, _position + new Vector2(-_offset, 0), Quaternion.Euler(0, 0, 90));
    //            }               
    //            previousPosition = _position + new Vector2(-_offset, 0);
    //            break;
    //        default:
    //            break;
    //    }

    //    previousTileRotation = randomTileRotation;

    //    //Middle tiles
    //    for (int tileIndex = 0; tileIndex < _tileCount; tileIndex++)
    //    {
    //        //type next tile 0 horizontal, 1 curve  
    //        randomTileNumber = UnityEngine.Random.Range(0, 2);

    //        switch (randomTileNumber)
    //        {
    //            case 0:
    //                switch (previousTileRotation)
    //                {
    //                    case 0:
    //                        //PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 0));
    //                        previousPosition = _position + new Vector2(0, _offset);
    //                        break;
    //                    case 1:
    //                        //PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, -90));
    //                        previousPosition = _position + new Vector2(_offset, 0);
    //                        break;
    //                    case 2:
    //                        //PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 180));
    //                        previousPosition = _position + new Vector2(0, -_offset);
    //                        break;
    //                    case 3:
    //                        //PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 90));
    //                        previousPosition = _position + new Vector2(-_offset, 0);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                //PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, 0));
    //                break;
    //            case 1:
    //                PlaceTile(_startTile, _position, Quaternion.Euler(0, 0, -90));
    //                break;
    //            default:
    //                break;
    //        }

    //    }   

    //    //End tile
    //}

    //private void PlaceTile(GameObject go, Vector2 position, Quaternion rotation)
    //{
    //    GameObject tile = Instantiate(go, position, rotation);
    //    tile.transform.SetParent(this.transform);
    //}

    public GameObject[] SpawnRooms;
    public GameObject[] EndRooms;
    public GameObject[] HorizontalRooms;
    public GameObject[] LWayRooms;

    public List<Vector2> _roomPositionList = new List<Vector2>();
    private List<GameObject> _roomList = new List<GameObject>();

    [SerializeField]
    public int NumberOfRooms = 5;

    private float _offset = 5f;

    private Vector2 _position = Vector2.zero;
    private Vector2 _lastPosition = Vector2.zero;

    private GameObject _room;

    private int _randomRoomIndex = 0;

    private void Start()
    {
        GeneratePositions();
        AddRooms();
    }

    private void AddRooms()
    {
        Vector2 previousPosition = Vector2.zero;
        Vector2 nextPosition = _roomPositionList[1];
        Vector2 position = _roomPositionList[0];

        Vector2 previousPositionCalculation;
        Vector2 nextPositionCalculation;

        _randomRoomIndex = UnityEngine.Random.Range(0, SpawnRooms.Length);

        if (nextPosition == Vector2.left * _offset)
        {
            _room = Instantiate(SpawnRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
        }
        else if (nextPosition == Vector2.right * _offset)
        {
            _room = Instantiate(SpawnRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, -90));
        }
        else if (nextPosition == Vector2.up * _offset)
        {
            _room = Instantiate(SpawnRooms[_randomRoomIndex], position, Quaternion.identity);
        }
        else if (nextPosition == Vector2.down * _offset)
        {
            _room = Instantiate(SpawnRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, -180));
        }

        _roomList.Add(_room);

        for (int positionIndex = 1; positionIndex < _roomPositionList.Count - 1; positionIndex++)
        {
            position = _roomPositionList[positionIndex];
            previousPosition = _roomPositionList[positionIndex - 1];
            nextPosition = _roomPositionList[positionIndex + 1];

            previousPositionCalculation = previousPosition - position;
            nextPositionCalculation = nextPosition - position;

            //links rechts
            if (previousPositionCalculation == Vector2.left * _offset && nextPositionCalculation == Vector2.right * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
            }
            else if (previousPositionCalculation == Vector2.right * _offset && nextPositionCalculation == Vector2.left * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
            }
            //boven onder
            else if (previousPositionCalculation == Vector2.up * _offset && nextPositionCalculation == Vector2.down * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.down * _offset && nextPositionCalculation == Vector2.up * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, HorizontalRooms.Length);
                _room = Instantiate(HorizontalRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            //links boven
            else if (previousPositionCalculation == Vector2.left * _offset && nextPositionCalculation == Vector2.up * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 180));
            }
            else if (previousPositionCalculation == Vector2.up * _offset && nextPositionCalculation == Vector2.left * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 180));
            }
            //links onder
            else if (previousPositionCalculation == Vector2.left * _offset && nextPositionCalculation == Vector2.down * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, -90));
            }
            else if (previousPositionCalculation == Vector2.down * _offset && nextPositionCalculation == Vector2.left * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, -90));
            }
            //Rechts boven
            else if (previousPositionCalculation == Vector2.right * _offset && nextPositionCalculation == Vector2.up * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
            }
            else if (previousPositionCalculation == Vector2.up * _offset && nextPositionCalculation == Vector2.right * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
            }
            //Rechts onder
            else if (previousPositionCalculation == Vector2.right * _offset && nextPositionCalculation == Vector2.down * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.identity);
            }
            else if (previousPositionCalculation == Vector2.down * _offset && nextPositionCalculation == Vector2.right * _offset)
            {
                _randomRoomIndex = UnityEngine.Random.Range(0, LWayRooms.Length);
                _room = Instantiate(LWayRooms[_randomRoomIndex], position, Quaternion.identity);
            }

            _roomList.Add(_room);
        }

        previousPosition = _roomPositionList[_roomPositionList.Count - 2];
        position = _roomPositionList[_roomPositionList.Count - 1];

        previousPositionCalculation = previousPosition - position;

        _randomRoomIndex = UnityEngine.Random.Range(0, EndRooms.Length);

        if (previousPositionCalculation == Vector2.left * _offset)
        {
            _room = Instantiate(EndRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 90));
        }
        else if (previousPositionCalculation == Vector2.right * _offset)
        {
            _room = Instantiate(EndRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, -90));
        }
        else if (previousPositionCalculation == Vector2.up * _offset)
        {
            _room = Instantiate(EndRooms[_randomRoomIndex], position, Quaternion.identity);
        }
        else if (previousPositionCalculation == Vector2.down * _offset)
        {
            _room = Instantiate(EndRooms[_randomRoomIndex], position, Quaternion.Euler(0, 0, 180));
        }

        _roomList.Add(_room);
    }

    private void GeneratePositions()
    {
        bool positionsGenerated = false;
        while (!positionsGenerated)
        {
            bool hasOverLap = false;
            _position = Vector3.zero;
            _lastPosition = Vector3.zero;
            //add the spawn position
            _roomPositionList.Add(_position);

            //add the other rooms position, including the end room
            for (int roomIndex = 0; roomIndex < NumberOfRooms; roomIndex++)
            {
                //do this calculation while the room position already exists
                int randomDirection = UnityEngine.Random.Range(0, 4);
                int counter = 0;
                do
                {
                    ++counter;
                    hasOverLap = false;
                    switch (randomDirection)
                    {
                        case 0:
                            _position = _lastPosition + Vector2.left * _offset;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 1:
                            _position = _lastPosition + Vector2.up * _offset;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 2:
                            _position = _lastPosition + Vector2.right * _offset;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                ++randomDirection;
                            }
                            break;
                        case 3:
                            _position = _lastPosition + Vector2.down * _offset;
                            if (IsOverlapping())
                            {
                                hasOverLap = true;
                                randomDirection = 0;
                            }
                            break;
                    }
                } while (hasOverLap && counter >= 4);

                if (hasOverLap)
                {
                    _roomPositionList.Clear();
                    break;
                }

                _roomPositionList.Add(_position);

                _lastPosition = _position;
            }
            if (!hasOverLap)
            {
                positionsGenerated = true;
            }
        }
    }
    bool IsOverlapping()
    {
        //Check if the roomposition already exists

        for (int positionIndex = 0; positionIndex < _roomPositionList.Count; positionIndex++)
        {
            if (_roomPositionList[positionIndex] == _position)
            {
                return true;
            }
        }
        return false;
    }
}
