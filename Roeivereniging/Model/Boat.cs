﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Boat
    {
        private int _id;
        public string name { get; }
        public string type { get; }
        public int capacity { get; }
        public bool steer { get; }
        public bool sculling { get; }
        private BoatType _category;

        public Boat(int id, string name, int capacity, int category, bool steer, bool sculling)
        {
            _id = id;
            this.name = name;
            type = "";
            _category = (BoatType)category;
            this.capacity = capacity;
            this.steer = steer;
            this.sculling = sculling;

            switch (_category)
            {
                case BoatType.A:
                    break;
                case BoatType.B:
                    type += "B";
                    break;
                case BoatType.C:
                    type += "C";
                    break;
                case BoatType.W:
                    type = "Wherry";
                    break;
                default:
                    break;
            }
            type += this.capacity;
            type += sculling ? "x" : "";
            type += steer ? "+" : "";
        }
        public String ToString()
        {
            return $"id:\t{_id}\t name:\t{name}\tcapacity:\t{capacity}\tsteer:\t{steer}\tsculling:\t{sculling}";
        }

    }
}