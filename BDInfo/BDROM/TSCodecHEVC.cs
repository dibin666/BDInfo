﻿//============================================================================
// BDInfo - Blu-ray Video and Audio Analysis Tool
// Copyright © 2010 Cinema Squid
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using System.Globalization;

namespace BDInfoLib.BDROM;

public abstract class TSCodecHEVC
{
    internal class MasteringMetadata2086
    {
        internal ushort[] Primaries;
        internal uint[] Luminance;

        public MasteringMetadata2086()
        {
            Primaries = new ushort[8];
            Luminance = new uint[2];
        }
    }

    internal class MasteringDisplayColorVolumeValue
    {
        internal byte Code; // ISO code
        internal ushort[] Values; // G, B, R, W pairs (x values then y values)

        public MasteringDisplayColorVolumeValue()
        {
            Code = 0;
            Values = new ushort[8];
        }
    };

    internal static MasteringDisplayColorVolumeValue[] MasteringDisplayColorVolumeValues =
    {
        new() { Code =  1, Values = new ushort[] {15000, 30000,  7500,  3000, 32000, 16500, 15635, 16450} }, // BT.709
        new() { Code =  9, Values = new ushort[] { 8500, 39850,  6550,  2300, 35400, 14600, 15635, 16450} }, // BT.2020
        new() { Code = 11, Values = new ushort[] {13250, 34500,  7500,  3000, 34000, 16000, 15700, 17550} }, // DCI P3
        new() { Code = 12, Values = new ushort[] {13250, 34500,  7500,  3000, 34000, 16000, 15635, 16450} }, // Display P3
    };

    internal class VideoParamSetStruct
    {
        internal ushort VPSMaxSubLayers;

        internal VideoParamSetStruct(ushort vpsMaxSubLayersMinus1)
        {
            VPSMaxSubLayers = vpsMaxSubLayersMinus1;
        }

        internal VideoParamSetStruct()
        {
            VPSMaxSubLayers = 0;
        }
    }

    internal class XXLData
    {
        internal ulong BitRateValue;
        internal ulong CPBSizeValue;
        internal bool CBRFlag;

        internal XXLData(ulong bitRateValue, ulong cpbSizeValue, bool cbrFlag)
        {
            BitRateValue = bitRateValue;
            CPBSizeValue = cpbSizeValue;
            CBRFlag = cbrFlag;
        }
    }

    internal class XXL
    {
        internal List<XXLData> SchedSel;

        internal XXL(List<XXLData> shedSel)
        {
            SchedSel = shedSel;
        }

        internal XXL()
        {
            SchedSel = new List<XXLData>();
        }
    }

    internal class XXLCommon
    {
        internal bool SubPicHRDParamsPresentFlag;
        internal ushort DuCPBRemovalDelayIncrementLengthMinus1;
        internal ushort DPBOutputDelayDULengthMinus1;
        internal ushort InitialCPBRemovalDelayLengthMinus1;
        internal ushort AUCPBRemovalDelayLengthMinus1;
        internal ushort DPBOutputDelayLengthMinus1;

        internal XXLCommon(bool subPicHRDParamsPresentFlag,
            ushort duCPBRemovalDelayIncrementLengthMinus1,
            ushort dpbOutputDelayDULengthMinus1,
            ushort initialCPBRemovalDelayLengthMinus1,
            ushort auCPBRemovalDelayLengthMinus1,
            ushort dpbOutputDelayLengthMinus1)
        {
            SubPicHRDParamsPresentFlag = subPicHRDParamsPresentFlag;
            DuCPBRemovalDelayIncrementLengthMinus1 = duCPBRemovalDelayIncrementLengthMinus1;
            DPBOutputDelayDULengthMinus1 = dpbOutputDelayDULengthMinus1;
            InitialCPBRemovalDelayLengthMinus1 = initialCPBRemovalDelayLengthMinus1;
            AUCPBRemovalDelayLengthMinus1 = auCPBRemovalDelayLengthMinus1;
            DPBOutputDelayLengthMinus1 = dpbOutputDelayLengthMinus1;
        }

        internal XXLCommon()
        {
            SubPicHRDParamsPresentFlag = false;
            DuCPBRemovalDelayIncrementLengthMinus1 = 0;
            DPBOutputDelayDULengthMinus1 = 0;
            InitialCPBRemovalDelayLengthMinus1 = 0;
            AUCPBRemovalDelayLengthMinus1 = 0;
            DPBOutputDelayLengthMinus1 = 0;
        }
    }

    internal class VUIParametersStruct
    {
        internal XXL NAL;
        internal XXL VCL;
        internal XXLCommon XXLCommon;
        internal uint NumUnitsInTick;
        internal uint TimeScale;
        internal ushort SarWidth;
        internal ushort SarHeight;
        internal byte AspectRatioIDC;
        internal byte VideoFormat;
        internal byte VideoFullRangeFlag;
        internal byte ColourPrimaries;
        internal byte TransferCharacteristics;
        internal byte MatrixCoefficients;
        internal bool AspectRatioInfoPresentFlag;
        internal bool VideoSignalTypePresentFlag;
        internal bool FrameFieldInfoPresentFlag;
        internal bool ColourDescriptionPresentFlag;
        internal bool TimingInfoPresentFlag;

        internal VUIParametersStruct(XXL nal,
            XXL vcl,
            XXLCommon xxlCommon,
            uint numUnitsInTick,
            uint timeScale,
            ushort sarWidth,
            ushort sarHeight,
            byte aspectRatioIDC,
            byte videoFormat,
            byte videoFullRangeFlag,
            byte colourPrimaries,
            byte transferCharacteristics,
            byte matrixCoefficients,
            bool aspectRatioInfoPresentFlag,
            bool videoSignalTypePresentFlag,
            bool frameFieldInfoPresentFlag,
            bool colourDescriptionPresentFlag,
            bool timingInfoPresentFlag)
        {
            NAL = nal;
            VCL = vcl;
            XXLCommon = xxlCommon;
            NumUnitsInTick = numUnitsInTick;
            TimeScale = timeScale;
            SarWidth = sarWidth;
            SarHeight = sarHeight;
            AspectRatioIDC = aspectRatioIDC;
            VideoFormat = videoFormat;
            VideoFullRangeFlag = videoFullRangeFlag;
            ColourPrimaries = colourPrimaries;
            TransferCharacteristics = transferCharacteristics;
            MatrixCoefficients = matrixCoefficients;
            AspectRatioInfoPresentFlag = aspectRatioInfoPresentFlag;
            VideoSignalTypePresentFlag = videoSignalTypePresentFlag;
            FrameFieldInfoPresentFlag = frameFieldInfoPresentFlag;
            ColourDescriptionPresentFlag = colourDescriptionPresentFlag;
            TimingInfoPresentFlag = timingInfoPresentFlag;
        }

        internal VUIParametersStruct()
        {
            NAL = new XXL();
            VCL = new XXL();
            XXLCommon = new XXLCommon();
            NumUnitsInTick = 0;
            TimeScale = 0;
            SarWidth = 0;
            SarHeight = 0;
            AspectRatioIDC = 0;
            VideoFormat = 0;
            VideoFullRangeFlag = 0;
            ColourPrimaries = 0;
            TransferCharacteristics = 0;
            MatrixCoefficients = 0;
            AspectRatioInfoPresentFlag = false;
            VideoSignalTypePresentFlag = false;
            FrameFieldInfoPresentFlag = false;
            ColourDescriptionPresentFlag = false;
            TimingInfoPresentFlag = false;
        }
    }

    internal class SeqParameterSetStruct
    {
        internal VUIParametersStruct VUIParameters;
        internal uint ProfileSpace;
        internal bool TierFlag;
        internal uint ProfileIDC;
        internal uint LevelIDC;
        internal uint PicWidthInLumaSamples;
        internal uint PicHeightInLumaSamples;
        internal uint ConfWinLeftOffset;
        internal uint ConfWinRightOffset;
        internal uint ConfWinTopOffset;
        internal uint ConfWinBottomOffset;
        internal byte VideoParameterSetID;
        internal byte ChromaFormatIDC;
        internal bool SeparateColourPlaneFlag;
        internal byte Log2MaxPicOrderCntLsbMinus4;
        internal byte BitDepthLumaMinus8;
        internal byte BitDepthChromaMinus8;
        internal bool GeneralProgressiveSourceFlag;
        internal bool GeneralInterlacedSourceFlag;
        internal bool GeneralFrameOnlyConstraintFlag;

        //computed
        internal bool NalHrdBpPresentFlag => VUIParameters.NAL != null;
        internal bool VclHrdPbPresentFlag => VUIParameters.VCL != null;
        internal bool CpbDpbDelaysPresentFlag => VUIParameters.XXLCommon != null;
        internal byte ChromaArrayType => SeparateColourPlaneFlag ? (byte)0 : ChromaFormatIDC;

        internal SeqParameterSetStruct(VUIParametersStruct vuiParameters,
            uint profileSpace,
            bool tierFlag,
            uint profileIDC,
            uint levelIDC,
            uint picWidthInLumaSamples,
            uint picHeightInLumaSamples,
            uint confWinLeftOffset,
            uint confWinRightOffset,
            uint confWinTopOffset,
            uint confWinBottomOffset,
            byte videoParameterSetID,
            byte chromaFormatIDC,
            bool separateColourPlaneFlag,
            byte log2MaxPicOrderCntLsbMinus4,
            byte bitDepthLumaMinus8,
            byte bitDepthChromaMinus8,
            bool generalProgressiveSourceFlag,
            bool generalInterlacedSourceFlag,
            bool generalFrameOnlyConstraintFlag)
        {
            VUIParameters = vuiParameters;
            ProfileSpace = profileSpace;
            TierFlag = tierFlag;
            ProfileIDC = profileIDC;
            LevelIDC = levelIDC;
            PicWidthInLumaSamples = picWidthInLumaSamples;
            PicHeightInLumaSamples = picHeightInLumaSamples;
            ConfWinLeftOffset = confWinLeftOffset;
            ConfWinRightOffset = confWinRightOffset;
            ConfWinTopOffset = confWinTopOffset;
            ConfWinBottomOffset = confWinBottomOffset;
            VideoParameterSetID = videoParameterSetID;
            ChromaFormatIDC = chromaFormatIDC;
            SeparateColourPlaneFlag = separateColourPlaneFlag;
            Log2MaxPicOrderCntLsbMinus4 = log2MaxPicOrderCntLsbMinus4;
            BitDepthLumaMinus8 = bitDepthLumaMinus8;
            BitDepthChromaMinus8 = bitDepthChromaMinus8;
            GeneralProgressiveSourceFlag = generalProgressiveSourceFlag;
            GeneralInterlacedSourceFlag = generalInterlacedSourceFlag;
            GeneralFrameOnlyConstraintFlag = generalFrameOnlyConstraintFlag;
        }

        internal SeqParameterSetStruct()
        {
            VUIParameters = new VUIParametersStruct();
            ProfileSpace = 0;
            TierFlag = false;
            ProfileIDC = 0;
            LevelIDC = 0;
            PicWidthInLumaSamples = 0;
            PicHeightInLumaSamples = 0;
            ConfWinLeftOffset = 0;
            ConfWinRightOffset = 0;
            ConfWinTopOffset = 0;
            ConfWinBottomOffset = 0;
            VideoParameterSetID = 0;
            ChromaFormatIDC = 0;
            SeparateColourPlaneFlag = false;
            Log2MaxPicOrderCntLsbMinus4 = 0;
            BitDepthLumaMinus8 = 0;
            BitDepthChromaMinus8 = 0;
            GeneralProgressiveSourceFlag = false;
            GeneralInterlacedSourceFlag = false;
            GeneralFrameOnlyConstraintFlag = false;
        }
    }

    internal class PicParameterSetStruct
    {
        internal byte SeqParameterSetID;
        internal byte NumRefIdxL0DefaultActiveMinus1;
        internal byte NumRefIdxL1DefaultActiveMinus1;
        internal byte NumExtraSliceHeaderBits;
        internal bool DependentSliceSegmentsEnabledFlag;

        internal PicParameterSetStruct(byte seqParameterSetID,
            byte numRefIdxL0DefaultActiveMinus1,
            byte numRefIdxL1DefaultActiveMinus1,
            byte numExtraSliceHeaderBits,
            bool dependentSliceSegmentsEnabledFlag)
        {
            SeqParameterSetID = seqParameterSetID;
            NumRefIdxL0DefaultActiveMinus1 = numRefIdxL0DefaultActiveMinus1;
            NumRefIdxL1DefaultActiveMinus1 = numRefIdxL1DefaultActiveMinus1;
            NumExtraSliceHeaderBits = numExtraSliceHeaderBits;
            DependentSliceSegmentsEnabledFlag = dependentSliceSegmentsEnabledFlag;
        }

        internal PicParameterSetStruct()
        {
            SeqParameterSetID = 0;
            NumRefIdxL0DefaultActiveMinus1 = 0;
            NumRefIdxL1DefaultActiveMinus1 = 0;
            NumExtraSliceHeaderBits = 0;
            DependentSliceSegmentsEnabledFlag = false;
        }
    }

    internal class ExtendedDataSet
    {
        internal List<VideoParamSetStruct> VideoParamSets;
        internal List<VUIParametersStruct> VUIParameterSets;
        internal List<SeqParameterSetStruct> SeqParameterSets;
        internal List<PicParameterSetStruct> PicParameterSets;

        internal string MasteringDisplayColorPrimaries;
        internal string MasteringDisplayLuminance;
        internal uint MaximumContentLightLevel;
        internal uint MaximumFrameAverageLightLevel;

        internal bool LightLevelAvailable;

        internal List<string> ExtendedFormatInfo;

        internal byte PreferredTransferCharacteristics;

        internal bool IsHdr10Plus;

        internal ExtendedDataSet()
        {
            VideoParamSets = new List<VideoParamSetStruct>();
            VUIParameterSets = new List<VUIParametersStruct>();
            SeqParameterSets = new List<SeqParameterSetStruct>();
            PicParameterSets = new List<PicParameterSetStruct>();

            MasteringDisplayColorPrimaries = string.Empty;
            MasteringDisplayLuminance = string.Empty;
            MaximumContentLightLevel = 0;
            MaximumFrameAverageLightLevel = 0;

            LightLevelAvailable = false;

            ExtendedFormatInfo = new List<string>();

            PreferredTransferCharacteristics = 2;

            IsHdr10Plus = false;
        }
    }

    private static string ColourPrimaries(byte colourPrimaries)
    {
        return colourPrimaries switch
        {
            1 => "BT.709",
            4 => "BT.470 System M",
            5 => "BT.601 PAL",
            6 => "BT.601 NTSC",
            7 => "SMPTE 240M", //Same as BT.601 NTSC
            8 => "Generic film",
            9 => "BT.2020", //Added in HEVC
            10 => "XYZ", //Added in HEVC 2014
            11 => "DCI P3", //Added in HEVC 2016
            12 => "Display P3", //Added in HEVC 2016
            22 => "EBU Tech 3213", //Added in HEVC 2016
            _ => ""
        };
    }

    private static string TransferCharacteristics(byte transferCharacteristics)
    {
        return transferCharacteristics switch
        {
            1 => "BT.709", //Same as BT.601
            4 => "BT.470 System M",
            5 => "BT.470 System B/G",
            6 => "BT.601",
            7 => "SMPTE 240M",
            8 => "Linear",
            9 => "Logarithmic (100:1)", //Added in MPEG-4 Visual
            10 => "Logarithmic (316.22777:1)", //Added in MPEG-4 Visual
            11 => "xvYCC", //Added in AVC
            12 => "BT.1361", //Added in AVC
            13 => "sRGB/sYCC", //Added in HEVC
            14 => "BT.2020 (10-bit)", //Same a BT.601, Added in HEVC, 10/12-bit difference is in ISO 23001-8
            15 => "BT.2020 (12-bit)", //Same a BT.601, Added in HEVC, 10/12-bit difference is in ISO 23001-8
            16 => "PQ", //Added in HEVC 2015
            17 => "SMPTE 428M", //Added in HEVC 2015
            18 => "HLG", //Added in HEVC 2016
            _ => ""
        };
    }

    private static string MatrixCoefficients(byte matrixCoefficients)
    {
        return matrixCoefficients switch
        {
            0 => "Identity", //Added in AVC
            1 => "BT.709",
            4 => "FCC 73.682",
            5 => "BT.470 System B/G",
            6 => "BT.601", //Same as BT.470 System B/G
            7 => "SMPTE 240M",
            8 => "YCgCo", //Added in AVC
            9 => "BT.2020 non-constant", //Added in HEVC
            10 => "BT.2020 constant", //Added in HEVC
            11 => "Y'D'zD'x", //Added in HEVC 2016
            12 => "Chromaticity-derived non-constant", //Added in HEVC 2016
            13 => "Chromaticity-derived constant", //Added in HEVC 2016
            14 => "ICtCp", //Added in HEVC 2016
            _ => ""
        };
    }

    private static uint _chromaSampleLocTypeTopField;
    private static uint _chromaSampleLocTypeBottomField;

    private static uint _profileSpace;
    private static bool _tierFlag;
    private static ushort _profileIDC;
    private static ushort _levelIDC;
    private static bool _generalProgressiveSourceFlag;
    private static bool _generalInterlacedSourceFlag;
    private static bool _generalFrameOnlyConstraintFlag;

    private static ExtendedDataSet _extendedData;

    private static List<VideoParamSetStruct> _videoParamSets;
    private static List<VUIParametersStruct> _vuiParameterSets;
    private static List<SeqParameterSetStruct> _seqParameterSets;
    private static List<PicParameterSetStruct> _picParameterSets;

    private static string _masteringDisplayColorPrimaries;
    private static string _masteringDisplayLuminance;
    private static uint _maximumContentLightLevel;
    private static uint _maximumFrameAverageLightLevel;

    private static List<string> _extendedFormatInfo;
    private static bool _lightLevelAvailable;

    private static byte _preferredTransferCharacteristics;

    private static bool _isHdr10Plus;

    private static bool _firstSliceSegmentInPicFlag;
    private static uint _slicePicParameterSetId;

    private static bool _isInitialized;
    private static bool _dependentSliceSegmentFlag;

    public static void Scan(TSVideoStream stream, TSStreamBuffer buffer, ref string tag)
    {
        _isInitialized = stream.IsInitialized;

        stream.ExtendedData ??= new ExtendedDataSet();

        _extendedData = (ExtendedDataSet)stream.ExtendedData;

        _videoParamSets = _extendedData.VideoParamSets;
        _vuiParameterSets = _extendedData.VUIParameterSets;
        _seqParameterSets = _extendedData.SeqParameterSets;
        _picParameterSets = _extendedData.PicParameterSets;

        _masteringDisplayColorPrimaries = _extendedData.MasteringDisplayColorPrimaries;
        _masteringDisplayLuminance = _extendedData.MasteringDisplayLuminance;

        _maximumContentLightLevel = _extendedData.MaximumContentLightLevel;
        _maximumFrameAverageLightLevel = _extendedData.MaximumFrameAverageLightLevel;

        _lightLevelAvailable = _extendedData.LightLevelAvailable;

        _preferredTransferCharacteristics = _extendedData.PreferredTransferCharacteristics;

        _extendedFormatInfo = _extendedData.ExtendedFormatInfo;

        var frameTypeRead = false;

        do
        {
            var syncByteFound = false;
            do
            {
                var streamPos = buffer.Position;
                if (buffer.ReadByte() == 0x0 &&
                    buffer.ReadByte() == 0x0 &&
                    buffer.ReadByte() == 0x0 &&
                    buffer.ReadByte() == 0x1)
                {
                    syncByteFound = true;
                    break;
                }

                buffer.BSSkipBytes((int)(streamPos - buffer.Position));

                if (buffer.ReadByte() == 0x0 &&
                    buffer.ReadByte() == 0x0 &&
                    buffer.ReadByte() == 0x1)
                {
                    syncByteFound = true;
                    break;
                }

                buffer.BSSkipBytes((int)(streamPos - buffer.Position + 1));

            } while (buffer.Position < buffer.Length - 3 &&
                     (!_isInitialized || _isInitialized && !frameTypeRead));

            if (buffer.Position >= buffer.Length || !syncByteFound) continue;

            var lastStreamPos = buffer.Position;

            buffer.BSSkipBits(1, true); // skip 1 bit

            long nalUnitType = buffer.ReadBits2(6, true);

            buffer.BSSkipBits(9, true); // nuh_layer_id (6), nuh_temporal_id_plus1 (3)

            switch (nalUnitType)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    tag = SliceSegmentLayer(buffer, nalUnitType);
                    frameTypeRead = tag != null;
                    break;
                case 32:
                    VideoParameterSet(buffer);
                    break;
                case 33:
                    SeqParameterSet(buffer);
                    break;
                case 34:
                    PicParameterSet(buffer);
                    break;
                case 35:
                    AccessUnitDelimiter(buffer);
                    break;
                case 39:
                case 40:
                    Sei(buffer);
                    break;
            }

            buffer.BSSkipNextByte();
            buffer.BSSkipBytes((int)(lastStreamPos - buffer.Position), true);
        } while (buffer.Position < buffer.Length - 3 &&
                 (!_isInitialized || _isInitialized && !frameTypeRead));

        _extendedData.PreferredTransferCharacteristics = _preferredTransferCharacteristics;

        _extendedData.LightLevelAvailable = _lightLevelAvailable;

        _extendedData.MaximumContentLightLevel = _maximumContentLightLevel;
        _extendedData.MaximumFrameAverageLightLevel = _maximumFrameAverageLightLevel;

        _extendedData.MasteringDisplayColorPrimaries = _masteringDisplayColorPrimaries;
        _extendedData.MasteringDisplayLuminance = _masteringDisplayLuminance;

        _extendedData.VideoParamSets = _videoParamSets;
        _extendedData.VUIParameterSets = _vuiParameterSets;
        _extendedData.SeqParameterSets = _seqParameterSets;
        _extendedData.PicParameterSets = _picParameterSets;

        _extendedData.IsHdr10Plus = _isHdr10Plus;

        stream.ExtendedData = _extendedData;

        // TODO: profile to string
        if (_seqParameterSets!.Count > 0 && !stream.IsInitialized)
        {
            var seqParameterSet = _seqParameterSets[0];
            if (seqParameterSet.ProfileSpace == 0)
            {
                var profile = string.Empty;
                if (seqParameterSet.ProfileIDC > 0)
                {
                    switch (seqParameterSet.ProfileIDC)
                    {
                        case 0:
                            profile = "No profile";
                            break;
                        case 1:
                            profile = "Main";
                            break;
                        case 2:
                            profile = "Main 10";
                            break;
                        case 3:
                            profile = "Main Still";
                            break;
                        default:
                            profile = "";
                            break;
                    }
                }
                if (seqParameterSet.LevelIDC > 0)
                {
                    var calcLevel = (float)seqParameterSet.LevelIDC / 30;
                    var dec = seqParameterSet.LevelIDC % 10;
                    profile += " @ Level " +
                               string.Format(CultureInfo.InvariantCulture, dec >= 1 ? "{0:0.0}" : "{0:0}", calcLevel) +
                               " @ ";
                    if (seqParameterSet.TierFlag) profile += "High";
                    else profile += "Main";
                }

                stream.EncodingProfile = profile;

                if (seqParameterSet.ChromaFormatIDC > 0)
                {
                    var chromaFormat = string.Empty;
                    switch (seqParameterSet.ChromaFormatIDC)
                    {
                        case 1:
                            chromaFormat = "4:2:0";
                            break;
                        case 2:
                            chromaFormat = "4:2:2";
                            break;
                        case 3:
                            chromaFormat = "4:4:4";
                            break;
                    }
                    if (chromaFormat != string.Empty && BDInfoLibSettings.ExtendedStreamDiagnostics)
                        _extendedFormatInfo!.Add(chromaFormat);
                }

                if (seqParameterSet.BitDepthLumaMinus8 == seqParameterSet.BitDepthChromaMinus8)
                    _extendedFormatInfo!.Add($"{seqParameterSet.BitDepthLumaMinus8 + 8} bits");

                if (seqParameterSet.BitDepthLumaMinus8 + 8 == 10 &&                 // 10 bit
                    seqParameterSet is
                    {
                        ChromaFormatIDC: 1,                         // ChromaFormat 4:2:0
                        VUIParameters: {
                            VideoSignalTypePresentFlag: true,
                            ColourDescriptionPresentFlag: true,
                            ColourPrimaries: 9,                     // ColourPrimaries BT.2020
                            TransferCharacteristics: 16,            // TransferCharacteristics PQ
                            MatrixCoefficients: 9 or 10             // MatrixCoefficients BT.2020 non-constant or constant
                        }
                    } && !string.IsNullOrEmpty(_masteringDisplayColorPrimaries))
                {
                    _extendedFormatInfo!.Add(stream.PID >= 4117 ? "Dolby Vision" : _isHdr10Plus ? "HDR10+" : "HDR10");
                }

                if (seqParameterSet.VUIParameters.VideoSignalTypePresentFlag)
                {
                    if (BDInfoLibSettings.ExtendedStreamDiagnostics)
                        _extendedFormatInfo!.Add(seqParameterSet.VUIParameters.VideoFullRangeFlag == 1 ? "Full Range" : "Limited Range");

                    if (seqParameterSet.VUIParameters.ColourDescriptionPresentFlag)
                    {
                        _extendedFormatInfo!.Add(ColourPrimaries(seqParameterSet.VUIParameters.ColourPrimaries));
                        if (BDInfoLibSettings.ExtendedStreamDiagnostics)
                        {
                            _extendedFormatInfo.Add(TransferCharacteristics(seqParameterSet.VUIParameters.TransferCharacteristics));
                            _extendedFormatInfo.Add(MatrixCoefficients(seqParameterSet.VUIParameters.MatrixCoefficients));

                        }
                    }
                }
            }
        }

        if (BDInfoLibSettings.ExtendedStreamDiagnostics && !stream.IsInitialized)
        {
            if (_masteringDisplayColorPrimaries != string.Empty)
            {
                _extendedFormatInfo!.Add("Mastering display color primaries: " + _masteringDisplayColorPrimaries);
            }
            if (_masteringDisplayLuminance != string.Empty)
            {
                _extendedFormatInfo!.Add("Mastering display luminance: " + _masteringDisplayLuminance);
            }

            if (_lightLevelAvailable && _maximumContentLightLevel > 0)
            {
                _extendedFormatInfo!.Add("Maximum Content Light Level: " + _maximumContentLightLevel + " cd / m2");
                _extendedFormatInfo!.Add("Maximum Frame-Average Light Level: " + _maximumFrameAverageLightLevel + " cd/m2");
            }
        }

        stream.IsVBR = true;
        if (_seqParameterSets.Count > 0)
            stream.IsInitialized = true;
    }

    private static string SliceSegmentLayer(TSStreamBuffer buffer, long nalUnitType)
    {
        // slice headers
        _dependentSliceSegmentFlag = false;

        _firstSliceSegmentInPicFlag = buffer.ReadBool();

        if (nalUnitType is >= 16 and <= 23)
            buffer.ReadBool(); // no_output_of_prior_pics_flag

        _slicePicParameterSetId = buffer.ReadExp(true);

        if (_slicePicParameterSetId >= _picParameterSets!.Count)
        {
            _slicePicParameterSetId = uint.MaxValue;
            return null;
        }

        if (!_firstSliceSegmentInPicFlag)
        {
            if (_picParameterSets[(int)_slicePicParameterSetId].DependentSliceSegmentsEnabledFlag)
            {
                _dependentSliceSegmentFlag = buffer.ReadBool(true);
            }
            return null;
        }

        if (_dependentSliceSegmentFlag)
            return null;

        buffer.BSSkipBits(_picParameterSets[(int)_slicePicParameterSetId].NumExtraSliceHeaderBits);
        var sliceType = buffer.ReadExp(true);

        return sliceType switch
        {
            0 => "P",
            1 => "B",
            2 => "I",
            _ => null
        };
    }

    // packet 32
    private static void VideoParameterSet(TSStreamBuffer buffer)
    {
        if (_isInitialized) return;

        int vpsVideoParameterSetID = buffer.ReadBits2(4, true);

        buffer.BSSkipBits(8, true); //vps_reserved_three_2bits, vps_reserved_zero_6bits

        var maxSubLayers = buffer.ReadBits2(3, true); //vps_max_sub_layers_minus1

        buffer.BSSkipBits(17, true); //vps_temporal_id_nesting_flag, vps_reserved_0xffff_16bits

        ProfileTierLevel(buffer, maxSubLayers);

        var tempB = buffer.ReadBool(true); //vps_sub_layer_ordering_info_present_flag
        for (var subLayerPos = tempB ? 0 : maxSubLayers; subLayerPos <= maxSubLayers; subLayerPos++)
        {
            buffer.SkipExpMulti(3, true); //vps_max_dec_pic_buffering_minus1, vps_max_num_reorder_pics, vps_max_latency_increase_plus1
        }

        var vpsMaxLayerID = buffer.ReadBits2(6, true);
        var vpsNumLayerSetsMinus1 = buffer.ReadExp(true); //vps_num_layer_sets_minus1

        for (var layerSetPos = 1; layerSetPos <= vpsNumLayerSetsMinus1; layerSetPos++)
        for (var layerId = 0; layerId <= vpsMaxLayerID; layerId++)
            buffer.BSSkipBits(1, true); //layer_id_included_flag

        var vpsTimingInfoPresentFlag = buffer.ReadBool(true);
        if (vpsTimingInfoPresentFlag)
        {
            buffer.BSSkipBits(64, true); //vps_num_units_in_tick, vps_time_scale

            var vpsPocProportionalToTimingFlag = buffer.ReadBool(true);
            if (!vpsPocProportionalToTimingFlag)
                buffer.SkipExp(true); //vps_num_ticks_poc_diff_one_minus1

            var vpsNumHRDParameters = (int)buffer.ReadExp(true);
            if (vpsNumHRDParameters > 1024) vpsNumHRDParameters = 0;
            for (var hrdPos = 0; hrdPos < vpsNumHRDParameters; hrdPos++)
            {
                XXLCommon xxlCommon = null;
                XXL nal = null;
                XXL vcl = null;

                buffer.SkipExp(true); //hrd_layer_sed_idx
                var cprmsPresentFlag = hrdPos <= 0 || buffer.ReadBool(true);
                HRDParameters(buffer, cprmsPresentFlag, vpsNumLayerSetsMinus1, ref xxlCommon, ref nal, ref vcl);
            }
        }
        buffer.BSSkipBits(1, true); //vps_extension_flag

        if (vpsVideoParameterSetID >= _videoParamSets!.Count)
            for (var i = _videoParamSets.Count - 1; i < vpsVideoParameterSetID; i++)
            {
                _videoParamSets.Add(new VideoParamSetStruct(0));
            }
        _videoParamSets[vpsVideoParameterSetID] = new VideoParamSetStruct((ushort)vpsNumLayerSetsMinus1);
    }

    // packet 33
    private static void SeqParameterSet(TSStreamBuffer buffer)
    {
        if (_isInitialized) return;

        var vuiParametersItem = new VUIParametersStruct();
        var videoParamSetItem = new VideoParamSetStruct();

        uint confWinLeftOffset = 0, confWinRightOffset = 0, confWinTopOffset = 0, confWinBottomOffset = 0;
        var separateColourPlaneFlag = false;

        uint videoParameterSetID = buffer.ReadBits2(4, true);

        if (videoParameterSetID >= _videoParamSets!.Count)
        {
            return;
        }

        uint maxSubLayersMinus1 = buffer.ReadBits2(3, true);
        buffer.BSSkipBits(1, true);
        ProfileTierLevel(buffer, maxSubLayersMinus1);
        var spsSeqParameterSetID = buffer.ReadExp(true);
        var chromaFormatIDC = buffer.ReadExp(true);
        switch (chromaFormatIDC)
        {
            case >= 4:
                return;
            case 3:
                separateColourPlaneFlag = buffer.ReadBool(true);
                break;
        }

        var picWidthInLumaSamples = buffer.ReadExp(true);
        var picHeightInLumaSamples = buffer.ReadExp(true);
        if (buffer.ReadBool(true)) //conformance_window_flag
        {
            confWinLeftOffset = buffer.ReadExp(true);
            confWinRightOffset = buffer.ReadExp(true);
            confWinTopOffset = buffer.ReadExp(true);
            confWinBottomOffset = buffer.ReadExp(true);
        }

        var bitDepthLumaMinus8 = buffer.ReadExp(true);
        if (bitDepthLumaMinus8 > 6)
            return;
        var bitDepthChromaMinus8 = buffer.ReadExp(true);
        if (bitDepthChromaMinus8 > 6)
            return;
        var log2MaxPicOrderCntLsbMinus4 = buffer.ReadExp(true);
        if (log2MaxPicOrderCntLsbMinus4 > 12)
            return;
        var spsSubLayerOrderingInfoPresentFlag = buffer.ReadBool(true);
        for (var subLayerPos = spsSubLayerOrderingInfoPresentFlag ? 0 : maxSubLayersMinus1; subLayerPos <= maxSubLayersMinus1; subLayerPos++)
        {
            buffer.SkipExpMulti(3, true);
        }
        buffer.SkipExpMulti(6, true);

        if (buffer.ReadBool(true)) //scaling_list_enabled_flag
            if (buffer.ReadBool(true)) //sps_scaling_list_data_present_flag
                ScalingListData(buffer);

        buffer.BSSkipBits(2, true);
        if (buffer.ReadBool(true)) //pcm_enabled_flag
        {
            buffer.BSSkipBits(8, true);
            buffer.SkipExpMulti(2, true);
            buffer.BSSkipBits(1, true);
        }
        var numShortTermRefPicSets = buffer.ReadExp(true);
        ShortTermRefPicSets(buffer, numShortTermRefPicSets);
        if (buffer.ReadBool(true)) //long_term_ref_pics_present_flag
        {
            var numLongTermRefPicsSps = buffer.ReadExp(true);
            for (var i = 0; i < numLongTermRefPicsSps; i++)
            {
                buffer.BSSkipBits((int)(log2MaxPicOrderCntLsbMinus4 + 4), true);
                buffer.BSSkipBits(1, true);
            }
        }
        buffer.BSSkipBits(2, true);
        if (buffer.ReadBool(true)) //vui_parameters_present_flag
        {
            VUIParameters(buffer, videoParamSetItem, ref vuiParametersItem);
        }

        if (spsSeqParameterSetID >= _seqParameterSets!.Count)
            for (var i = _seqParameterSets.Count - 1; i < spsSeqParameterSetID; i++)
            {
                _seqParameterSets.Add(new SeqParameterSetStruct());
            }
        _seqParameterSets[(int)spsSeqParameterSetID] = new SeqParameterSetStruct(vuiParametersItem,
                                                                                _profileSpace,
                                                                                _tierFlag,
                                                                                _profileIDC,
                                                                                _levelIDC,
                                                                                picWidthInLumaSamples,
                                                                                picHeightInLumaSamples,
                                                                                confWinLeftOffset,
                                                                                confWinRightOffset,
                                                                                confWinTopOffset,
                                                                                confWinBottomOffset,
                                                                                (byte)videoParameterSetID,
                                                                                (byte)chromaFormatIDC,
                                                                                separateColourPlaneFlag,
                                                                                (byte)log2MaxPicOrderCntLsbMinus4,
                                                                                (byte)bitDepthLumaMinus8,
                                                                                (byte)bitDepthChromaMinus8,
                                                                                _generalProgressiveSourceFlag,
                                                                                _generalInterlacedSourceFlag,
                                                                                _generalFrameOnlyConstraintFlag);
    }

    // packet 34
    private static void PicParameterSet(TSStreamBuffer buffer)
    {
        if (_isInitialized) return;

        var ppsPicParameterSetID = buffer.ReadExp(true);
        if (ppsPicParameterSetID >= 64)
            return;
        var ppsSeqParameterSetID = buffer.ReadExp(true);
        if (ppsSeqParameterSetID >= 16)
            return;
        if (ppsSeqParameterSetID >= _seqParameterSets!.Count)
            return;

        var dependentSliceSegmentsEnabledFlag = buffer.ReadBool(true);
        buffer.BSSkipBits(1, true);
        var numExtraSliceHeaderBits = (byte)buffer.ReadBits2(3, true);
        buffer.BSSkipBits(2, true);
        var numRefIdxL0DefaultActiveMinus1 = buffer.ReadExp(true);
        var numRefIdxL1DefaultActiveMinus1 = buffer.ReadExp(true);
        buffer.SkipExp(true);
        buffer.BSSkipBits(2, true);
        if (buffer.ReadBool(true)) //cu_qp_delta_enabled_flag
            buffer.SkipExp(true); //diff_cu_qp_delta_depth
        buffer.SkipExpMulti(2, true);
        buffer.BSSkipBits(4, true);
        var tilesEnabledFlag = buffer.ReadBool(true);
        buffer.BSSkipBits(1, true);
        if (tilesEnabledFlag)
        {
            var numTileColumnsMinus1 = buffer.ReadExp(true);
            var numTileRowsMinus1 = buffer.ReadExp(true);
            var uniformSpacingFlag = buffer.ReadBool(true);
            if (!uniformSpacingFlag)
            {
                buffer.SkipExpMulti((int)numTileColumnsMinus1, true);
                buffer.SkipExpMulti((int)numTileRowsMinus1, true);
            }
            buffer.BSSkipBits(1, true);
        }
        buffer.BSSkipBits(1, true);
        if (buffer.ReadBool(true)) //deblocking_filter_control_present_flag
        {
            buffer.BSSkipBits(1, true);
            if (!buffer.ReadBool(true)) //pps_disable_deblocking_filter_flag
            {
                buffer.SkipExpMulti(2, true);
            }
        }
        if (buffer.ReadBool(true)) //pps_scaling_list_data_present_flag
            ScalingListData(buffer);
        buffer.BSSkipBits(1, true);
        buffer.SkipExp(true);
        buffer.BSSkipBits(1, true);
        if (buffer.ReadBool(true)) //pps_extension_flag
            buffer.BSSkipNextByte();

        if (ppsPicParameterSetID >= _picParameterSets!.Count)
            for (var i = _picParameterSets.Count - 1; i < ppsPicParameterSetID; i++)
                _picParameterSets.Add(new PicParameterSetStruct());
        _picParameterSets[(int)ppsPicParameterSetID] = new PicParameterSetStruct((byte)ppsSeqParameterSetID,
                                                                                (byte)numRefIdxL0DefaultActiveMinus1,
                                                                                (byte)numRefIdxL1DefaultActiveMinus1,
                                                                                numExtraSliceHeaderBits,
                                                                                dependentSliceSegmentsEnabledFlag);

    }

    // packet 35
    private static void AccessUnitDelimiter(TSStreamBuffer buffer)
    {
        buffer.BSSkipBits(3, true);
    }

    // packet 39 & 40
    private static void Sei(TSStreamBuffer buffer)
    {
        if (_isInitialized) return;

        var elementStart = buffer.Position;

        int numBytes;

        // find element end
        do
        {
            var streamPos = buffer.Position;
            numBytes = 0;
            if (buffer.ReadByte() == 0x0 &&
                buffer.ReadByte() == 0x0 &&
                buffer.ReadByte() == 0x0 &&
                buffer.ReadByte() == 0x1)
            {
                numBytes = 4;
                break;
            }

            buffer.BSSkipBytes((int)(streamPos - buffer.Position));
            if (buffer.ReadByte() == 0x0 &&
                buffer.ReadByte() == 0x0 &&
                buffer.ReadByte() == 0x1)
            {
                numBytes = 3;
                break;
            }
            buffer.BSSkipBytes((int)(streamPos - buffer.Position + 1));
        } while (buffer.Position < buffer.Length);

        var elementSize = buffer.Position - elementStart;

        buffer.BSSkipBytes((int)(elementSize * -1));

        elementSize -= numBytes + 1;

        do
        {
            var seqParameterSetID = uint.MaxValue;
            uint payloadType = 0, payloadSize = 0;
            byte payloadTypeByte, payloadSizeByte;

            do
            {
                payloadTypeByte = buffer.ReadByte(true);
                payloadType += payloadTypeByte;
            } while (payloadTypeByte == 0xFF);
            do
            {
                payloadSizeByte = buffer.ReadByte(true);
                payloadSize += payloadSizeByte;
            } while (payloadSizeByte == 0xFF);

            var savedPos = (ulong)buffer.Position + payloadSize;

            if (savedPos > (ulong)buffer.Length) // wrong length
                return;

            switch (payloadType)
            {
                case 0:
                    SeiMessageBufferingPeriod(buffer, ref seqParameterSetID, payloadSize);
                    break;
                case 1:
                    SeiMessagePicTiming(buffer, ref seqParameterSetID, payloadSize);
                    break;
                case 6:
                    SeiMessageRecoveryPoint(buffer);
                    break;
                case 129:
                    SeiMessageActiveParametersSets(buffer);
                    break;
                case 137:
                    SeiMessageMasteringDisplayColourVolume(buffer);
                    break;
                case 144:
                    SeiMessageLightLevel(buffer);
                    break;
                case 147:
                    SeiAlternativeTransferCharacteristics(buffer);
                    break;
                case 4: // SEI_USER_DATA_REGISTERED_ITU_T_T35
                    SeiUserDataRegisteredItuTt35(buffer, payloadSize);
                    break;
                default:
                    buffer.BSSkipBytes((int)payloadSize, true);
                    break;
            }
            if (savedPos > (ulong)buffer.Position)
                buffer.BSSkipBytes((int)(savedPos - (ulong)buffer.Position), true);
        } while (buffer.Position < elementStart + elementSize);
    }

    // SEI - 4 SEI_USER_DATA_REGISTERED_ITU_T_T35
    private static void SeiUserDataRegisteredItuTt35(TSStreamBuffer buffer, uint payloadSize)
    {
        var countryCode = buffer.ReadBits2(8, true);                      // itu_t_t35_country_code
        var terminalProviderCode = buffer.ReadBits2(16, true);           // itu_t_t35_terminal_provider_code
        var terminalProviderOrientedCode = buffer.ReadBits2(16, true);  // itu_t_t35_terminal_provider_oriented_code
        var applicationID = buffer.ReadBits4(8, true);                      // application_identifier
        var applicationVersion = buffer.ReadBits4(8, true);                 // application_version
        var numWindows = buffer.ReadBits4(2, true);                         // num_windows
        buffer.BSSkipBits(6, true);
        // ST 2094-40 page 4
        if (countryCode == 0xB5 && terminalProviderCode == 0x003C && terminalProviderOrientedCode == 0x0001)
        {
            // ST 2094-40 application #4 page 4
            if (applicationID == 4 && applicationVersion is 0 or 1 && numWindows == 1)
            {
                _isHdr10Plus = true;
            }
        }
        payloadSize -= 8;
        buffer.BSSkipBytes((int)payloadSize, true);
    }

    // SEI - 0
    private static void SeiMessageBufferingPeriod(TSStreamBuffer buffer, ref uint seqParameterSetID, uint payloadSize)
    {
        seqParameterSetID = buffer.ReadExp(true);

        SeqParameterSetStruct seqParameterSetItem;
        if (seqParameterSetID >= _seqParameterSets!.Count || (seqParameterSetItem = _seqParameterSets[(int)seqParameterSetID]) == null)
        {
            buffer.BSSkipBits((int)(payloadSize * 8), true);
            return;
        }

        var subPicHRDParamsPresentFlag = false; //Default
        var irapCPBParamsPresentFlag = seqParameterSetItem.VUIParameters.XXLCommon?.SubPicHRDParamsPresentFlag ?? false;
        if (!subPicHRDParamsPresentFlag)
            irapCPBParamsPresentFlag = buffer.ReadBool(true);
        var auCPBRemovalDelayLengthMinus1 = (byte)(seqParameterSetItem.VUIParameters.XXLCommon?.AUCPBRemovalDelayLengthMinus1 ?? 23);
        var dpbOutputDelayLengthMinus1 = (byte)(seqParameterSetItem.VUIParameters.XXLCommon?.DPBOutputDelayLengthMinus1 ?? 23);

        if (irapCPBParamsPresentFlag)
        {
            buffer.BSSkipBits(auCPBRemovalDelayLengthMinus1 + dpbOutputDelayLengthMinus1 + 2, true);
        }

        buffer.BSSkipBits(auCPBRemovalDelayLengthMinus1 + 2, true); //concatenation_flag, au_cpb_removal_delay_delta_minus1

        if (seqParameterSetItem.NalHrdBpPresentFlag)
            SeiMessageBufferingPeriodXXL(buffer, seqParameterSetItem.VUIParameters.XXLCommon, irapCPBParamsPresentFlag, seqParameterSetItem.VUIParameters.NAL, payloadSize);

        if (seqParameterSetItem.VclHrdPbPresentFlag)
            SeiMessageBufferingPeriodXXL(buffer, seqParameterSetItem.VUIParameters.XXLCommon, irapCPBParamsPresentFlag, seqParameterSetItem.VUIParameters.VCL, payloadSize);
    }

    private static void SeiMessageBufferingPeriodXXL(TSStreamBuffer buffer, XXLCommon xxlCommon, bool irapCPBParamsPresentFlag, XXL xxl, uint payloadSize)
    {
        if (xxlCommon == null || xxl == null)
        {
            buffer.BSSkipBits((int)(payloadSize * 8), true);
            return;
        }
        for (var schedSelIdx = 0; schedSelIdx < xxl.SchedSel.Count; schedSelIdx++)
        {
            buffer.BSSkipBits(xxlCommon.InitialCPBRemovalDelayLengthMinus1 + 1, true); //initial_cpb_removal_delay
            buffer.BSSkipBits(xxlCommon.InitialCPBRemovalDelayLengthMinus1 + 1, true); //initial_cpb_removal_delay_offset

            if (!xxlCommon.SubPicHRDParamsPresentFlag && !irapCPBParamsPresentFlag) continue;

            buffer.BSSkipBits(xxlCommon.InitialCPBRemovalDelayLengthMinus1 + 1, true); //initial_alt_cpb_removal_delay
            buffer.BSSkipBits(xxlCommon.InitialCPBRemovalDelayLengthMinus1 + 1, true); //initial_alt_cpb_removal_delay_offset
        }
    }

    // SEI - 1
    private static void SeiMessagePicTiming(TSStreamBuffer buffer, ref uint seqParameterSetID, uint payloadSize)
    {
        if (seqParameterSetID == uint.MaxValue && _seqParameterSets!.Count == 1)
            seqParameterSetID = 0;
        SeqParameterSetStruct seqParameterSetItem;
        if (seqParameterSetID >= _seqParameterSets!.Count || (seqParameterSetItem = _seqParameterSets[(int)seqParameterSetID]) == null)
        {
            buffer.BSSkipBits((int)(payloadSize * 8), true);
            return;
        }

        if (seqParameterSetItem.VUIParameters?.FrameFieldInfoPresentFlag ??
            seqParameterSetItem is { GeneralProgressiveSourceFlag: true, GeneralInterlacedSourceFlag: true })
        {
            buffer.BSSkipBits(7, true); //pic_struct, source_scan_type, duplicate_flag
        }

        if (!seqParameterSetItem.CpbDpbDelaysPresentFlag) return;

        var auCPBRemovalDelayLengthMinus1 = (byte)seqParameterSetItem.VUIParameters!.XXLCommon!.AUCPBRemovalDelayLengthMinus1;
        var dpbOutputDelayLengthMinus1 = (byte)seqParameterSetItem.VUIParameters.XXLCommon.DPBOutputDelayLengthMinus1;
        var subPicHRDParamsPresentFlag = seqParameterSetItem.VUIParameters.XXLCommon.SubPicHRDParamsPresentFlag;
        buffer.BSSkipBits(auCPBRemovalDelayLengthMinus1 + dpbOutputDelayLengthMinus1 + 2, true);

        if (!subPicHRDParamsPresentFlag) return;

        var dpbOutputDelayDULengthMinus1 = (byte)seqParameterSetItem.VUIParameters.XXLCommon.DPBOutputDelayDULengthMinus1;

        buffer.BSSkipBits(dpbOutputDelayDULengthMinus1 + 1, true);
    }

    // SEI - 6
    private static void SeiMessageRecoveryPoint(TSStreamBuffer buffer)
    {
        buffer.SkipExp(true); //recovery_poc_cnt
        buffer.BSSkipBits(2, true); //exact_match_flag, broken_link_flag
    }

    // SEI - 129
    private static void SeiMessageActiveParametersSets(TSStreamBuffer buffer)
    {
        buffer.BSSkipBits(6, true);
        var numSpsIdsMinus1 = buffer.ReadExp(true);
        buffer.SkipExpMulti((int)(numSpsIdsMinus1 + 1), true);
    }

    // SEI - 137
    private static void SeiMessageMasteringDisplayColourVolume(TSStreamBuffer buffer)
    {
        // TODO: Color Reading sometimes off.
        var meta = new MasteringMetadata2086();
        buffer.BSResetBits();
        for (var i = 0; i < 3; i++)
        {
            meta.Primaries[i * 2] = buffer.ReadBits2(16, true); // x
            meta.Primaries[i * 2 + 1] = buffer.ReadBits2(16, true); // y
        }
        meta.Primaries[3 * 2] = buffer.ReadBits2(16, true);
        meta.Primaries[3 * 2 + 1] = buffer.ReadBits2(16, true);

        meta.Luminance[1] = buffer.ReadBits4(32, true);
        meta.Luminance[0] = buffer.ReadBits4(32, true);

        //Reordering to RGB
        int r = 4, g = 4, b = 4;
        for (var c = 0; c < 3; c++)
        {
            if (meta.Primaries[c * 2] < 17500 && meta.Primaries[c * 2 + 1] < 17500)
                b = c;
            else if (meta.Primaries[c * 2 + 1] - meta.Primaries[c * 2] >= 0)
                g = c;
            else
                r = c;
        }
        if ((r | b | g) >= 4)
        {
            //Order not automatically detected, betting on GBR order
            g = 0;
            b = 1;
            r = 2;
        }

        var notValid = false;

        for (var c = 0; c < 8; c++)
        {
            if (meta.Primaries[c] == ushort.MaxValue)
                notValid = true;
        }

        _masteringDisplayColorPrimaries = string.Empty;
        var humanReadablePrimaries = false;

        if (!notValid)
        {
            foreach (var colorVolume in MasteringDisplayColorVolumeValues)
            {
                var code = colorVolume.Code;
                for (var j = 0; j < 2; j++)
                {
                    // +/- 0.0005 (3 digits after comma)
                    if (meta.Primaries[g * 2 + j] < colorVolume.Values[0 * 2 + j] - 25 || meta.Primaries[g * 2 + j] >= colorVolume.Values[0 * 2 + j] + 25)
                        code = 0;
                    if (meta.Primaries[b * 2 + j] < colorVolume.Values[1 * 2 + j] - 25 || meta.Primaries[b * 2 + j] >= colorVolume.Values[1 * 2 + j] + 25)
                        code = 0;
                    if (meta.Primaries[r * 2 + j] < colorVolume.Values[2 * 2 + j] - 25 || meta.Primaries[r * 2 + j] >= colorVolume.Values[2 * 2 + j] + 25)
                        code = 0;
                    // +/- 0.00005 (4 digits after comma)
                    if (meta.Primaries[3 * 2 + j] < colorVolume.Values[3 * 2 + j] - 2 || meta.Primaries[3 * 2 + j] >= colorVolume.Values[3 * 2 + j] + 3)
                        code = 0;
                }

                if (code <= 0) continue;

                _masteringDisplayColorPrimaries = ColourPrimaries(code);
                humanReadablePrimaries = true;
                break;
            }

            if (!humanReadablePrimaries)
            {
                _masteringDisplayColorPrimaries += FormattableString.Invariant(
                    $"R: x={(double)meta.Primaries[r * 2] / 50000:0.000000} y={(double)meta.Primaries[r * 2 + 1] / 50000:0.000000}, ");
                _masteringDisplayColorPrimaries += FormattableString.Invariant(
                    $"G: x={(double)meta.Primaries[g * 2] / 50000:0.000000} y={(double)meta.Primaries[g * 2 + 1] / 50000:0.000000}, ");
                _masteringDisplayColorPrimaries += FormattableString.Invariant(
                    $"B: x={(double)meta.Primaries[b * 2] / 50000:0.000000} y={(double)meta.Primaries[b * 2 + 1] / 50000:0.000000}, ");
                _masteringDisplayColorPrimaries += FormattableString.Invariant(
                    $"White point: x={(double)meta.Primaries[3 * 2] / 50000:0.000000} y={(double)meta.Primaries[3 * 2 + 1] / 50000:0.000000}");
            }
        }

        _masteringDisplayLuminance =
            FormattableString.Invariant(
                $"min: {(double)meta.Luminance[0] / 10000:0.0000} cd/m2, max: {(double)meta.Luminance[1] / 10000:0.0000} cd/m2");
    }

    // SEI - 144
    private static void SeiMessageLightLevel(TSStreamBuffer buffer)
    {
        _maximumContentLightLevel = buffer.ReadBits2(16, true);
        _maximumFrameAverageLightLevel = buffer.ReadBits2(16, true);
        _lightLevelAvailable = true;
    }

    // SEI - 147
    private static void SeiAlternativeTransferCharacteristics(TSStreamBuffer buffer)
    {
        _preferredTransferCharacteristics = (byte)buffer.ReadBits2(8, true);
    }

    private static void VUIParameters(TSStreamBuffer buffer, VideoParamSetStruct videoParamSetItem, ref VUIParametersStruct vuiParametersItem)
    {
        var xxlCommon = new XXLCommon();
        var nal = new XXL();
        var vcl = new XXL();

        uint numUnitsInTick = uint.MaxValue, timeScale = uint.MaxValue;
        ushort sarWidth = ushort.MaxValue, sarHeight = ushort.MaxValue;
        byte aspectRatioIDC = 0, videoFormat = 5, videoFullRangeFlag = 0, colourPrimaries = 2, transferCharacteristics = 2, matrixCoefficients = 2;
        var colourDescriptionPresentFlag = false;

        var aspectRatioInfoPresentFlag = buffer.ReadBool(true);
        if (aspectRatioInfoPresentFlag)
        {
            aspectRatioIDC = (byte)buffer.ReadBits2(8, true);
            if (aspectRatioIDC == 0xFF)
            {
                sarWidth = (ushort)buffer.ReadBits4(16, true);
                sarHeight = (ushort)buffer.ReadBits4(16, true);
            }
        }
        if (buffer.ReadBool(true)) //overscan_info_present_flag
            buffer.BSSkipBits(1, true);
        var videoSignalTypePresentFlag = buffer.ReadBool(true);
        if (videoSignalTypePresentFlag)
        {
            videoFormat = (byte)buffer.ReadBits2(3, true);
            videoFullRangeFlag = (byte)buffer.ReadBits2(1, true);
            colourDescriptionPresentFlag = buffer.ReadBool(true);
            if (colourDescriptionPresentFlag)
            {
                colourPrimaries = (byte)buffer.ReadBits2(8, true);
                transferCharacteristics = (byte)buffer.ReadBits2(8, true);
                matrixCoefficients = (byte)buffer.ReadBits2(8, true);
            }
        }
        if (buffer.ReadBool(true)) //chroma_loc_info_present_flag
        {
            _chromaSampleLocTypeTopField = buffer.ReadExp(true);
            _chromaSampleLocTypeBottomField = buffer.ReadExp(true);
        }
        buffer.BSSkipBits(2, true);
        var frameFieldInfoPresentFlag = buffer.ReadBool(true);
        if (buffer.ReadBool(true)) //default_display_window_flag
        {
            buffer.SkipExpMulti(4, true);
        }
        var timingInfoPresentFlag = buffer.ReadBool(true);
        if (timingInfoPresentFlag)
        {
            numUnitsInTick = (uint)buffer.ReadBits8(32, true);
            timeScale = (uint)buffer.ReadBits8(32, true);
            if (buffer.ReadBool(true)) //vui_poc_proportional_to_timing_flag
            {
                buffer.SkipExp(true);
            }
            if (buffer.ReadBool(true)) //hrd_parameters_present_flag
            {
                HRDParameters(buffer, true, videoParamSetItem.VPSMaxSubLayers, ref xxlCommon, ref nal, ref vcl);
            }
        }
        if (buffer.ReadBool(true)) //bitstream_restriction_flag
        {
            buffer.BSSkipBits(3, true);
            buffer.SkipExpMulti(5, true);
        }

        vuiParametersItem = new VUIParametersStruct(nal, vcl, xxlCommon, numUnitsInTick, timeScale, sarWidth, sarHeight, aspectRatioIDC, videoFormat,
            videoFullRangeFlag, colourPrimaries, transferCharacteristics, matrixCoefficients, aspectRatioInfoPresentFlag,
            videoSignalTypePresentFlag, frameFieldInfoPresentFlag, colourDescriptionPresentFlag, timingInfoPresentFlag);
    }

    private static void ShortTermRefPicSets(TSStreamBuffer buffer, uint numShortTermRefPicSets)
    {
        uint numPics = 0;
        for (var stRpsIdx = 0; stRpsIdx < numShortTermRefPicSets; stRpsIdx++)
        {
            var interRefPicSetPredictionFlag = false;
            if (stRpsIdx > 0)
                interRefPicSetPredictionFlag = buffer.ReadBool(true);

            if (interRefPicSetPredictionFlag)
            {
                uint deltaIdxMinus1 = 0;
                if (stRpsIdx == numShortTermRefPicSets)
                    deltaIdxMinus1 = buffer.ReadExp(true);
                if (deltaIdxMinus1 + 1 > stRpsIdx)
                {
                    return;
                }

                buffer.BSSkipBits(1, true);
                buffer.SkipExp(true);

                uint numPicsNew = 0;
                for (uint picPos = 0; picPos <= numPics; picPos++)
                {
                    if (buffer.ReadBool(true)) //used_by_curr_pic_flag
                        numPicsNew++;
                    else
                    {
                        if (buffer.ReadBool(true)) //use_delta_flag
                            numPicsNew++;
                    }
                }
                numPics = numPicsNew;
            }
            else
            {
                var numNegativePics = buffer.ReadExp(true);
                var numPositivePics = buffer.ReadExp(true);
                numPics = numNegativePics + numPositivePics;
                for (var i = 0; i < numNegativePics; i++)
                {
                    buffer.SkipExp(true);
                    buffer.BSSkipBits(1, true);
                }
                for (var i = 0; i < numPositivePics; i++)
                {
                    buffer.SkipExp(true);
                    buffer.BSSkipBits(1, true);
                }
            }
        }
    }

    private static void ScalingListData(TSStreamBuffer buffer)
    {
        for (var sizeId = 0; sizeId < 4; sizeId++)
        {
            for (var matrixId = 0; matrixId < (sizeId == 3 ? 2 : 6); matrixId++)
            {
                if (!buffer.ReadBool(true)) // scaling_list_pred_mode_flag
                    buffer.SkipExp(true);
                else
                {
                    var coefNum = Math.Min(64, 1 << 4 + (sizeId << 1));
                    if (sizeId > 1)
                        buffer.SkipExp(true);
                    for (var i = 0; i < coefNum; i++)
                        buffer.SkipExp(true);
                }
            }
        }
    }

    private static void ProfileTierLevel(TSStreamBuffer buffer, uint subLayerCount)
    {
        _profileSpace = buffer.ReadBits2(2, true);
        _tierFlag = buffer.ReadBool(true);
        _profileIDC = buffer.ReadBits2(5, true);

        buffer.BSSkipBits(32, true); // general_profile_compatibility_flags

        _generalProgressiveSourceFlag = buffer.ReadBool(true);
        _generalInterlacedSourceFlag = buffer.ReadBool(true);
        buffer.BSSkipBits(1, true); // general_non_packed_constraint_flag
        _generalFrameOnlyConstraintFlag = buffer.ReadBool(true);

        buffer.BSSkipBits(44, true); // general_reserved_zero_44bits

        _levelIDC = buffer.ReadBits2(8, true);

        var subLayerProfilePresentFlags = new List<bool>();
        var subLayerLevelPresentFlags = new List<bool>();

        for (var subLayerPos = 0; subLayerPos < subLayerCount; subLayerPos++)
        {
            var subLayerProfilePresentFlag = buffer.ReadBool(true);
            var subLayerLevelPresentFlag = buffer.ReadBool(true);

            subLayerProfilePresentFlags.Add(subLayerProfilePresentFlag);
            subLayerLevelPresentFlags.Add(subLayerLevelPresentFlag);
        }

        if (subLayerCount > 0)
        {
            buffer.BSSkipBits(2, true); //reserved_zero_2bits
        }

        for (var subLayerPos = 0; subLayerPos < subLayerCount; subLayerPos++)
        {
            if (subLayerProfilePresentFlags[subLayerPos])
            {
                buffer.BSSkipBits(88, true); // sub layer profile data
            }
            if (subLayerLevelPresentFlags[subLayerPos])
            {
                buffer.BSSkipBits(8, true); //sub_layer_level_idc
            }
        }
    }

    private static void HRDParameters(TSStreamBuffer buffer,
        bool commonInfPresentFlag,
        uint maxNumSubLayersMinus1,
        ref XXLCommon xxlCommon,
        ref XXL nal,
        ref XXL vcl)
    {
        byte bitRateScale = 0, cpbSizeScale = 0, duCPBRemovalDelayIncrementLengthMinus1 = 0;
        byte dpbOutputDelayDULengthMinus1 = 0, initialCPBRemovalDelayLengthMinus1 = 0;
        byte auCPBRemovalDelayLengthMinus1 = 0, dpbOutputDelayLengthMinus1 = 0;
        bool nalHRDParametersPresentFlag = false, vclHRDParametersPresentFlag = false;
        var subPicHRDParamsPresentFlag = false;

        if (commonInfPresentFlag)
        {
            nalHRDParametersPresentFlag = buffer.ReadBool(true);
            vclHRDParametersPresentFlag = buffer.ReadBool(true);
            if (nalHRDParametersPresentFlag || vclHRDParametersPresentFlag)
            {
                subPicHRDParamsPresentFlag = buffer.ReadBool(true);
                if (subPicHRDParamsPresentFlag)
                {
                    buffer.BSSkipBits(8, true); //tick_divisor_minus2
                    duCPBRemovalDelayIncrementLengthMinus1 = (byte)buffer.ReadBits2(5, true);
                    buffer.BSSkipBits(1, true); //sub_pic_cpb_params_in_pic_timing_sei_flag
                    dpbOutputDelayDULengthMinus1 = (byte)buffer.ReadBits2(5, true);
                }
                bitRateScale = (byte)buffer.ReadBits2(4, true);
                cpbSizeScale = (byte)buffer.ReadBits2(4, true);
                if (subPicHRDParamsPresentFlag)
                    buffer.BSSkipBits(4, true); //cpb_size_du_scale
                initialCPBRemovalDelayLengthMinus1 = (byte)buffer.ReadBits2(5, true);
                auCPBRemovalDelayLengthMinus1 = (byte)buffer.ReadBits2(5, true);
                dpbOutputDelayLengthMinus1 = (byte)buffer.ReadBits2(5, true);
            }
        }

        for (byte numSubLayer = 0; numSubLayer <= maxNumSubLayersMinus1; numSubLayer++)
        {
            uint cpbCntMinus1 = 0;
            bool fixedPicRateWithinCvsFlag = true, lowDelayHRDFlag = false;
            var fixedPicRateGeneralFlag = buffer.ReadBool(true);
            if (!fixedPicRateGeneralFlag)
                fixedPicRateWithinCvsFlag = buffer.ReadBool(true);
            if (fixedPicRateWithinCvsFlag)
                buffer.SkipExp(true); //elemental_duration_in_tc_minus1
            else
                lowDelayHRDFlag = buffer.ReadBool(true);
            if (!lowDelayHRDFlag)
            {
                cpbCntMinus1 = buffer.ReadExp(true);
                if (cpbCntMinus1 > 31)
                {
                    return;
                }
            }
            if (nalHRDParametersPresentFlag || vclHRDParametersPresentFlag)
            {
                xxlCommon = new XXLCommon(subPicHRDParamsPresentFlag,
                    duCPBRemovalDelayIncrementLengthMinus1,
                    dpbOutputDelayDULengthMinus1,
                    initialCPBRemovalDelayLengthMinus1,
                    auCPBRemovalDelayLengthMinus1,
                    dpbOutputDelayLengthMinus1);
            }
            if (nalHRDParametersPresentFlag)
            {
                SubLayerHrdParameters(buffer, xxlCommon, bitRateScale, cpbSizeScale, cpbCntMinus1, ref nal);
            }
            if (vclHRDParametersPresentFlag)
            {
                SubLayerHrdParameters(buffer, xxlCommon, bitRateScale, cpbSizeScale, cpbCntMinus1, ref vcl);
            }
        }
    }

    private static void SubLayerHrdParameters(TSStreamBuffer buffer,
        XXLCommon xxlCommon,
        byte bitRateScale,
        byte cpbSizeScale,
        uint cpbCntMinus1,
        ref XXL hrdParametersItem)
    {
        var schedSel = new List<XXLData>((int)cpbCntMinus1 + 1);
        for (byte schedSelIdx = 0; schedSelIdx <= cpbCntMinus1; ++schedSelIdx)
        {
            var bitRateValueMinus1 = buffer.ReadExp(true);
            var bitRateValue = (ulong)((bitRateValueMinus1 + 1) * Math.Pow(2.0, 6 + bitRateScale));
            var cpbSizeValueMinus1 = buffer.ReadExp(true);
            var cpbSizeValue = (ulong)((cpbSizeValueMinus1 + 1) * Math.Pow(2.0, 4 + cpbSizeScale));
            if (xxlCommon!.SubPicHRDParamsPresentFlag)
            {
                buffer.SkipExpMulti(2, true); //cpb_size_du_value_minus1, bit_rate_du_value_minus1
            }
            var cbrFlag = buffer.ReadBool(true);
            schedSel.Add(new XXLData(bitRateValue, cpbSizeValue, cbrFlag));
        }

        hrdParametersItem = new XXL(schedSel);
    }
}