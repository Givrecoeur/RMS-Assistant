using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_Assistant
{
    public static class RMSNodeNameConstants
    {

        //Sections
        public static Dictionary<string, int[]> Sections = new Dictionary<string, int[]>
        {
            { "PLAYER_SETUP",          new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "LAND_GENERATION",       new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "ELEVATION_GENERATION",  new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "CLIFF_GENERATION",      new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "TERRAIN_GENERATION",    new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "CONNECTION_GENERATION", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "OBJECTS_GENERATION",    new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };


        //Conditions
        public static Dictionary<string, int[]> Conditionals = new Dictionary<string, int[]>
        {
            { "Condition", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> Conditions = new Dictionary<string, int[]>
        {
            { "if",     new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "elseif", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "else",   new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        //Randoms
        public static Dictionary<string, int[]> Randoms = new Dictionary<string, int[]>
        {
            { "start_random", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> Weigths = new Dictionary<string, int[]>
        {
            { "percent_chance", new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} }
        };

        //Constants&Defines
        public static Dictionary<string, int[]> Constants = new Dictionary<string, int[]>
        {
            { "Constant", new int[10] {1, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> Defines = new Dictionary<string, int[]>
        {
            { "Define", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        //Commands
        public static Dictionary<string, int[]> CommandsLand = new Dictionary<string, int[]>
        {
            { "create_player_lands", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "create_land",         new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> CommandsElevation = new Dictionary<string, int[]>
        {
            { "create_elevation", new int[10] {0, 1, 1, 7, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> CommandsTerrain = new Dictionary<string, int[]>
        {
            { "create_terrain", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> CommandsConnection = new Dictionary<string, int[]>
        {
            { "create_connect_all_players_land",  new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "create_connect_teams_lands",       new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "create_connect_all_lands",         new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "create_connect_to_nonplayer_land", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "create_connect_same_land_zones",   new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> CommandsObject = new Dictionary<string, int[]>
        {
            { "create_object", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        //Properties
        public static Dictionary<string, int[]> PropertiesPlayer = new Dictionary<string, int[]>
        {
            { "random_placement",       new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "grouped_by_team",        new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "direct_placement",       new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "nomad_resources",        new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_gaia_civilization",  new int[10] {0, 1, 0, 35, 0, 0, 0, 0, 0, 0} },
            { "effect_amount",          new int[10] {0, 4, 0, 0, 0, 0, 0, 0, 0, 0} }, //ask about this line
            { "weather_type",           new int[10] {0, 4, 0, 0, 0, 0, 0, 0, 0, 0} }, //ask about this line too
            { "ai_info_map_type",       new int[10] {1, 3, 0, 0, 0, 1, 0, 1, 0, 1} }
        };

        public static Dictionary<string, int[]> PropertiesLand = new Dictionary<string, int[]>
        {
            { "base_terrain", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "enable_waves", new int[10] {0, 1, 0, 1, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesTerrain = new Dictionary<string, int[]>
        {
            { "color_correction", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreatePlayerLands = new Dictionary<string, int[]>
        {
            { "terrain_type",                  new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "land_percent",                  new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "number_of_tiles",               new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "base_size",                     new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "base_elevation",                new int[10] {0, 1, 1, 7, 0, 0, 0, 0, 0, 0} },
            { "circle_radius",                 new int[10] {0, 2, 0, 50, 0, 50, 0, 0, 0, 0} },
            { "left_border",                   new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "right_border",                  new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "top_border",                    new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "bottom_border",                 new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "border_fuzziness",              new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "clumping_factor",               new int[10] {0, 1, -100, 99, 0, 0, 0, 0, 0, 0} },
            { "zone",                          new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_zone_randomly",             new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_zone_by_team",              new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "other_zone_avoidance_distance", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreateLand = new Dictionary<string, int[]>
        {
            { "terrain_type",                  new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "land_percent",                  new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "number_of_tiles",               new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "base_size",                     new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "base_elevation",                new int[10] {0, 1, 1, 7, 0, 0, 0, 0, 0, 0} },
            { "left_border",                   new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "right_border",                  new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "top_border",                    new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "bottom_border",                 new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "land_position",                 new int[10] {0, 2, 0, 100, 0, 99, 0, 0, 0, 0} },
            { "border_fuzziness",              new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "clumping_factor",               new int[10] {0, 1, -100, 99, 0, 0, 0, 0, 0, 0} },
            { "zone",                          new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_zone_randomly",             new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_zone_by_team",              new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "other_zone_avoidance_distance", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "min_placement_distance",        new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "land_id",                       new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "assign_to_player",              new int[10] {0, 1, 1, 8, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreateElevation = new Dictionary<string, int[]>
        {
            { "enable_balanced_elevation", new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "base_terrain",              new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "number_of_tiles",           new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "number_of_clumps",          new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_scale_by_size",         new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_scale_by_groups",       new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "spacing",                   new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCliffGeneration = new Dictionary<string, int[]>
        {
            { "min_number_of_cliffs", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "max_number_of_cliffs", new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} },
            { "min_length_of_cliff",  new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} },
            { "max_length_of_cliff",  new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} },
            { "cliff_curliness",      new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "min_distance_cliffs",  new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "min_terrain_distance", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreateTerrain = new Dictionary<string, int[]>
        {
            { "base_terrain",                   new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "land_percent",                   new int[10] {0, 1, 0, 100, 0, 0, 0, 0, 0, 0} },
            { "number_of_tiles",                new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "number_of_clumps",               new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_scale_by_size",              new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_scale_by_groups",            new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "spacing_to_other_terrain_types", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_avoid_player_start_areas",   new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "height_limits",                  new int[10] {0, 2, 0, 7, 0, 7, 0, 0, 0, 0} },
            { "set_flat_terrain_only",          new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "clumping_factor",                new int[10] {0, 1, -100, 99, 0, 0, 0, 0, 0, 0} },
            { "terrain_mask",                   new int[10] {0, 1, 1, 2, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreateConnect = new Dictionary<string, int[]>
        {
            { "default_terrain_replacement", new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "replace_terrain",             new int[10] {2, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "terrain_cost",                new int[10] {1, 1, 0, 0, 1, 15, 0, 0, 0, 0} },
            { "terrain_size",                new int[10] {1, 2, 0, 0, 0, 1000000, 0, 1000000, 0, 0} }
        };

        public static Dictionary<string, int[]> PropertiesCreateObject = new Dictionary<string, int[]>
        {
            { "number_of_objects",                 new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} },
            { "number_of_groups",                  new int[10] {0, 1, 1, 1000000, 0, 0, 0, 0, 0, 0} },
            { "group_variance",                    new int[10] {0, 1, -1000000, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_scaling_to_map_size",           new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_scaling_to_player_number",      new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_place_for_every_player",        new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_gaia_object_only",              new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_gaia_unconvertible",            new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "terrain_to_place_on",               new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "layer_to_place_on",                 new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "min_distance_to_players",           new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "max_distance_to_players",           new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "max_distance_to_other_zones",       new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "min_distance_group_placement",      new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "temp_min_distance_group_placement", new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "group_placement_radius",            new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "actor_area",                        new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "actor_area_radius",                 new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "actor_area_to_place_in",            new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "avoid_actor_area",                  new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "avoid_all_actor_area",              new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "set_tight_grouping",                new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "set_loose_grouping",                new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "force_placement",                   new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "place_on_forest_zone",              new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "avoid_forest_zone",                 new int[10] {0, 1, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "avoid_cliff_zone",                  new int[10] {0, 1, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "find_closest",                      new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "place_on_specific_land_id",         new int[10] {0, 1, 0, 1000000, 0, 0, 0, 0, 0, 0} },
            { "second_object",                     new int[10] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
            { "resource_delta",                    new int[10] {0, 1, -1000000, 1000000, 0, 0, 0, 0, 0, 0} }
        };

        public static Dictionary<string, int[]> FromNameGetDictCommand(string name)
        {
            if      (name == "LAND_GENERATION")       { return CommandsLand; }
            else if (name == "ELEVATION_GENERATION")  { return CommandsElevation; }
            else if (name == "TERRAIN_GENERATION")    { return CommandsTerrain; }
            else if (name == "CONNECTION_GENERATION") { return CommandsConnection; }
            else if (name == "OBJECTS_GENERATION")    { return CommandsObject; }
            else { return null/*throw new Exception("Impossible affiliation for a command Node")*/; }
        }

        public static List<string> FromNameGetListCommand(string name)
        {
            return FromNameGetDictCommand(name).Keys.ToList();
        }

        public static Dictionary<string, int[]> FromNameGetDictProperty(string name)
        {
            if      (name == "PLAYER_SETUP")                   { return PropertiesPlayer; }
            else if (name == "LAND_GENERATION")                { return PropertiesLand; }
            else if (name == "TERRAIN_GENERATION")             { return PropertiesTerrain; }
            else if (name == "CLIFF_GENERATION")               { return PropertiesCliffGeneration; }
            else if (name == "create_player_lands")            { return PropertiesCreatePlayerLands; }
            else if (name == "create_land")                    { return PropertiesCreateLand; }
            else if (name == "create_elevation")               { return PropertiesCreateElevation; }
            else if (name == "create_terrain")                 { return PropertiesCreateTerrain; }
            else if (name == "create_connect_all_players_land"
                  || name == "create_connect_teams_lands"
                  || name == "create_connect_all_lands"
                  || name == "create_connect_to_nonplayer_land"
                  || name == "create_connect_same_land_zones") { return PropertiesCreateConnect; }
            else if (name == "create_object")                  { return PropertiesCreateObject; }
            else { return null/*throw new Exception("Impossible affiliation for a property Node")*/; }
        }

        public static List<string> FromNameGetListProperty(string name)
        {
            return FromNameGetDictProperty(name).Keys.ToList();
        }

    }
}
