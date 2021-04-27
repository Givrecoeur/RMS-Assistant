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
        public static Dictionary<string, int> Sections = new Dictionary<string, int>
        {
            { "PLAYER_SETUP",          0 },
            { "LAND_GENERATION",       0 },
            { "ELEVATION_GENERATION",  0 },
            { "CLIFF_GENERATION",      0 },
            { "TERRAIN_GENERATION",    0 },
            { "CONNECTION_GENERATION", 0 },
            { "OBJECTS_GENERATION",    0 }
        };


        //Conditions
        public static Dictionary<string, int> Conditionals = new Dictionary<string, int>
        {
            { "Condition", 0 }
        };

        public static Dictionary<string, int> Conditions = new Dictionary<string, int>
        {
            { "if",     1 },
            { "elseif", 1 },
            { "else",   0 }
        };

        //Randoms
        public static Dictionary<string, int> Randoms = new Dictionary<string, int>
        {
            { "start_random", 0 }
        };

        public static Dictionary<string, int> Weigths = new Dictionary<string, int>
        {
            { "percent_chance", 1 }
        };

        //Constants&Defines
        public static Dictionary<string, int> Constants = new Dictionary<string, int>
        {
            { "Constant", 2 }
        };

        public static Dictionary<string, int> Defines = new Dictionary<string, int>
        {
            { "Define", 1 }
        };

        //Commands
        public static Dictionary<string, int> CommandsLand = new Dictionary<string, int>
        {
            { "create_player_lands", 0 },
            { "create_land",         0 }
        };

        public static Dictionary<string, int> CommandsElevation = new Dictionary<string, int>
        {
            { "create_elevation", 1 }
        };

        public static Dictionary<string, int> CommandsTerrain = new Dictionary<string, int>
        {
            { "create_terrain", 1 }
        };

        public static Dictionary<string, int> CommandsConnection = new Dictionary<string, int>
        {
            { "create_connect_all_players_land",  0 },
            { "create_connect_teams_lands",       0 },
            { "create_connect_all_lands",         0 },
            { "create_connect_to_nonplayer_land", 0 },
            { "create_connect_same_land_zones",   0 }
        };

        public static Dictionary<string, int> CommandsObject = new Dictionary<string, int>
        {
            { "create_object", 1 }
        };

        //Properties
        public static Dictionary<string, int> PropertiesPlayer = new Dictionary<string, int>
        {
            { "random_placement",       0 },
            { "grouped_by_team",        0 },
            { "direct_placement",       0 },
            { "nomad_resources",        0 },
            { "ai_info_map_type",       4 },
            { "set_gaia_civilization",  1 },
            { "effect_amount",          4 }, //ask about this line
            { "effect_percent",         4 }, //ask about this line
            { "weather_type",           4 }, //ask about this line too
            { "guard_state",            4 }, //ask about this line too
            { "terrain_state",          4 }, //ask about this line too
            { "behavior_version",       1 }  //ask about this line too
            
        };

        public static Dictionary<string, int> PropertiesLand = new Dictionary<string, int>
        {
            { "base_terrain", 1 },
            { "base_layer",   1 },
            { "enable_waves", 1 }
        };

        public static Dictionary<string, int> PropertiesTerrain = new Dictionary<string, int>
        {
            { "color_correction", 1 }
        };

        public static Dictionary<string, int> PropertiesCreatePlayerLands = new Dictionary<string, int>
        {
            { "terrain_type",                  1 },
            { "land_percent",                  1 },
            { "number_of_tiles",               1 },
            { "base_size",                     1 },
            { "base_elevation",                1 },
            { "circle_radius",                 2 },
            { "left_border",                   1 },
            { "right_border",                  1 },
            { "top_border",                    1 },
            { "bottom_border",                 1 },
            { "border_fuzziness",              1 },
            { "clumping_factor",               1 },
            { "zone",                          1 },
            { "set_zone_randomly",             0 },
            { "set_zone_by_team",              0 },
            { "other_zone_avoidance_distance", 1 }
        };

        public static Dictionary<string, int> PropertiesCreateLand = new Dictionary<string, int>
        {
            { "terrain_type",                  1 },
            { "land_percent",                  1 },
            { "number_of_tiles",               1 },
            { "base_size",                     1 },
            { "base_elevation",                1 },
            { "left_border",                   1 },
            { "right_border",                  1 },
            { "top_border",                    1 },
            { "bottom_border",                 1 },
            { "land_position",                 2 },
            { "border_fuzziness",              1 },
            { "clumping_factor",               1 },
            { "zone",                          1 },
            { "set_zone_randomly",             0 },
            { "set_zone_by_team",              0 },
            { "other_zone_avoidance_distance", 1 },
            { "min_placement_distance",        1 },
            { "land_id",                       1 },
            { "assign_to_player",              1 }
        };

        public static Dictionary<string, int> PropertiesCreateElevation = new Dictionary<string, int>
        {
            { "enable_balanced_elevation", 0 },
            { "base_terrain",              1 },
            { "number_of_tiles",           1 },
            { "number_of_clumps",          1 },
            { "set_scale_by_size",         0 },
            { "set_scale_by_groups",       0 },
            { "spacing",                   1 }
        };

        public static Dictionary<string, int> PropertiesCliffGeneration = new Dictionary<string, int>
        {
            { "min_number_of_cliffs", 1 },
            { "max_number_of_cliffs", 1 },
            { "min_length_of_cliff",  1 },
            { "max_length_of_cliff",  1 },
            { "cliff_curliness",      1 },
            { "min_distance_cliffs",  1 },
            { "min_terrain_distance", 1 }
        };

        public static Dictionary<string, int> PropertiesCreateTerrain = new Dictionary<string, int>
        {
            { "base_terrain",                   1 },
            { "land_percent",                   1 },
            { "number_of_tiles",                1 },
            { "number_of_clumps",               1 },
            { "set_scale_by_size",              0 },
            { "set_scale_by_groups",            0 },
            { "spacing_to_other_terrain_types", 1 },
            { "set_avoid_player_start_areas",   0 },
            { "height_limits",                  2 },
            { "set_flat_terrain_only",          0 },
            { "clumping_factor",                1 },
            { "terrain_mask",                   1 }
        };

        public static Dictionary<string, int> PropertiesCreateConnect = new Dictionary<string, int>
        {
            { "default_terrain_replacement", 1 },
            { "replace_terrain",             2 },
            { "terrain_cost",                2 },
            { "terrain_size",                3 }
        };

        public static Dictionary<string, int> PropertiesCreateObject = new Dictionary<string, int>
        {
            { "number_of_objects",                 1 },
            { "number_of_groups",                  1 },
            { "group_variance",                    1 },
            { "set_scaling_to_map_size",           0 },
            { "set_scaling_to_player_number",      0 },
            { "set_place_for_every_player",        0 },
            { "set_gaia_object_only",              0 },
            { "set_gaia_unconvertible",            0 },
            { "terrain_to_place_on",               1 },
            { "layer_to_place_on",                 1 },
            { "min_distance_to_players",           1 },
            { "max_distance_to_players",           1 },
            { "max_distance_to_other_zones",       1 },
            { "min_distance_group_placement",      1 },
            { "temp_min_distance_group_placement", 1 },
            { "group_placement_radius",            1 },
            { "actor_area",                        1 },
            { "actor_area_radius",                 1 },
            { "actor_area_to_place_in",            1 },
            { "avoid_actor_area",                  1 },
            { "avoid_all_actor_areas",             1 },
            { "set_tight_grouping",                0 },
            { "set_loose_grouping",                0 },
            { "force_placement",                   0 },
            { "place_on_forest_zone",              0 },
            { "avoid_forest_zone",                 1 },
            { "avoid_cliff_zone",                  1 },
            { "find_closest",                      0 },
            { "place_on_specific_land_id",         1 },
            { "second_object",                     1 },
            { "resource_delta",                    1 }
        };

        public static Dictionary<string, int> FromNameGetDictCommand(string name)
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

        public static Dictionary<string, int> FromNameGetDictProperty(string name)
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
