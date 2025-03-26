using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pak_Maker.Commands;
using Pak_Maker.Content;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using Pak_Maker.BytesEngine;
using Pak_Maker.Data;
using System.Diagnostics.Eventing.Reader;

namespace Pak_Maker.VM
{
    public class PakManagerVM : INotifyPropertyChanged
    {

        private float _playerFov;
        private float _sprintFov;
        private float _blunderbussFov;
        private float _eyeOfReachFov;
        private float _pistolFov;
        private float _cannonFov;
        private float _wheelFov;
        private float _mapFov;

        private bool _enableFieldOfView;
        private bool _enableStaticFov;

        private bool _quickswap;
        private bool _fastShot;
        private bool _fastReload;
        private bool _instantZoom;
        private bool _noSpreadPistol;
        private bool _reducedSpreadBlunder;
        private bool _wallbang;


        private bool _isFovVisible;
        private bool _isMiscVisible;
        private bool _isWorldVisible;
        private bool _isSettingsVisible;
        private bool _isGalleryVisible;

        private bool _removeFog;

        private bool _enableSkyColor;
        private System.Windows.Media.Color _skyColor;

        private float _skyR;
        private float _skyG;
        private float _skyB;

        private bool _enableWaterColor;
        private System.Windows.Media.Color _waterColor;

        private float _waterR;
        private float _waterG;
        private float _waterB;

        private bool _enableColorBullets;
        private System.Windows.Media.Color _bulletColor;

        private float _bulletR;
        private float _bulletG;
        private float _bulletB;
        private float _bulletA;

        private bool _enableSunColor;
        private System.Windows.Media.Color _sunColor;

        private float _sunR;
        private float _sunG;
        private float _sunB;
        private float _sunA;

        private bool _enableFogColor;
        private System.Windows.Media.Color _fogColor;

        private float _fogR;
        private float _fogG;
        private float _fogB;
        private float _fogA;

        private bool _enableMoonColor;
        private System.Windows.Media.Color _moonColor;

        private float _moonR;
        private float _moonG;
        private float _moonB;
        private float _moonA;

        public bool Wallbang
        {
            get => _wallbang;
            set { _wallbang = value; OnPropertyChanged(); }
        }
        public bool NoSpreadPistol
        {
            get => _noSpreadPistol;
            set { _noSpreadPistol = value; OnPropertyChanged(); }

        }
        public bool ReducedSpreadBlunder
        {
            get => _reducedSpreadBlunder;
            set { _reducedSpreadBlunder = value; OnPropertyChanged(); }

        }
        public Color BulletColor
        {
            get => _bulletColor;
            set
            {
                if (_bulletColor != value)
                {
                    _bulletColor = value;
                    OnPropertyChanged(nameof(BulletColor));
                    UpdateBulletColorValues();
                    OnPropertyChanged(nameof(BulletBrush));
                }
            }
        }
        public float BulletR
        {
            get => _bulletR;
            set
            {
                if (_bulletR != value)
                {
                    _bulletR = value;
                    OnPropertyChanged(nameof(BulletR));
                    UpdateBulletColorFromRgb();
                }
            }
        }
        public float BulletG
        {
            get => _bulletG;
            set
            {
                if (_bulletG != value)
                {
                    _bulletG = value;
                    OnPropertyChanged(nameof(BulletG));
                    UpdateBulletColorFromRgb();
                }
            }
        }
        public float BulletB
        {
            get => _bulletB;
            set
            {
                if (_bulletB != value)
                {
                    _bulletB = value;
                    OnPropertyChanged(nameof(BulletB));
                    UpdateBulletColorFromRgb();
                }
            }
        }
        public float BulletA
        {
            get => _bulletA;
            set
            {
                if (_bulletA != value)
                {
                    _bulletA = value;
                    OnPropertyChanged(nameof(BulletA));
                    UpdateBulletColorFromRgb();
                }
            }
        }

        public SolidColorBrush FogBrush => new SolidColorBrush(FogColor);
        public SolidColorBrush MoonBrush => new SolidColorBrush(MoonColor);
        public SolidColorBrush BulletBrush => new SolidColorBrush(BulletColor);
        public SolidColorBrush SkyBrush => new SolidColorBrush(SkyColor);
        public SolidColorBrush WaterBrush => new SolidColorBrush(WaterColor);
        public SolidColorBrush SunBrush => new SolidColorBrush(SunColor);

        public Color SunColor
        {
            get => _sunColor;
            set
            {
                if (_sunColor != value)
                {
                    _sunColor = value;
                    OnPropertyChanged(nameof(SunColor));
                    UpdateSunColorValues();
                    OnPropertyChanged(nameof(SunBrush));
                }
            }
        }

        public float SunR
        {
            get => _sunR;
            set
            {
                if (_sunR != value)
                {
                    _sunR = value;
                    OnPropertyChanged(nameof(SunR));
                    UpdateSunColorFromRgb();
                }
            }
        }

        public float SunG
        {
            get => _sunG;
            set
            {
                if (_sunG != value)
                {
                    _sunG = value;
                    OnPropertyChanged(nameof(SunG));
                    UpdateSunColorFromRgb();
                }
            }
        }

        public float SunB
        {
            get => _sunB;
            set
            {
                if (_sunB != value)
                {
                    _sunB = value;
                    OnPropertyChanged(nameof(SunB));
                    UpdateSunColorFromRgb();
                }
            }
        }

        public float SunA
        {
            get => _sunA;
            set
            {
                if (_sunA != value)
                {
                    _sunA = value;
                    OnPropertyChanged(nameof(SunA));
                    UpdateSunColorFromRgb();
                }
            }
        }

        private void UpdateSunColorFromRgb()
        {
            _sunColor = Color.FromArgb(
                (byte)(_sunA * 255),
                (byte)(_sunR * 255),
                (byte)(_sunG * 255),
                (byte)(_sunB * 255)
            );
            OnPropertyChanged(nameof(SunColor));
            OnPropertyChanged(nameof(SunBrush));
        }

        private void UpdateSunColorValues()
        {
            SunR = _sunColor.R / 255f;
            SunG = _sunColor.G / 255f;
            SunB = _sunColor.B / 255f;
            SunA = _sunColor.A / 255f;
        }

        public bool EnableSunColor
        {
            get => _enableSunColor;
            set
            {
                if (_enableSunColor != value)
                {
                    _enableSunColor = value;
                    OnPropertyChanged(nameof(EnableSunColor));
                }
            }
        }

        public Color MoonColor
        {
            get => _moonColor;
            set
            {
                if (_moonColor != value)
                {
                    _moonColor = value;
                    OnPropertyChanged(nameof(MoonColor));
                    UpdateMoonColorValues();
                    OnPropertyChanged(nameof(MoonBrush));
                }
            }
        }

        public float MoonR
        {
            get => _moonR;
            set
            {
                if (_moonR != value)
                {
                    _moonR = value;
                    OnPropertyChanged(nameof(MoonR));
                    UpdateMoonColorFromRgb();
                }
            }
        }

        public float MoonG
        {
            get => _moonG;
            set
            {
                if (_moonG != value)
                {
                    _moonG = value;
                    OnPropertyChanged(nameof(MoonG));
                    UpdateMoonColorFromRgb();
                }
            }
        }

        public float MoonB
        {
            get => _moonB;
            set
            {
                if (_moonB != value)
                {
                    _moonB = value;
                    OnPropertyChanged(nameof(MoonB));
                    UpdateMoonColorFromRgb();
                }
            }
        }

        public float MoonA
        {
            get => _moonA;
            set
            {
                if (_moonA != value)
                {
                    _moonA = value;
                    OnPropertyChanged(nameof(MoonA));
                    UpdateMoonColorFromRgb();
                }
            }
        }

        private void UpdateMoonColorFromRgb()
        {
            _moonColor = Color.FromArgb(
                (byte)(_moonA * 255),
                (byte)(_moonR * 255),
                (byte)(_moonG * 255),
                (byte)(_moonB * 255)
            );
            OnPropertyChanged(nameof(MoonColor));
            OnPropertyChanged(nameof(MoonBrush));
        }

        private void UpdateMoonColorValues()
        {
            MoonR = _moonColor.R / 255f;
            MoonG = _moonColor.G / 255f;
            MoonB = _moonColor.B / 255f;
            MoonA = _moonColor.A / 255f;
        }

        public bool EnableMoonColor
        {
            get => _enableMoonColor;
            set
            {
                if (_enableMoonColor != value)
                {
                    _enableMoonColor = value;
                    OnPropertyChanged(nameof(EnableMoonColor));
                }
            }
        }


        private bool _removeunderwaterfog;
        public bool RemoveUnderwaterFog
        {
            get => _removeunderwaterfog;
            set
            {
                _removeunderwaterfog = value;
                OnPropertyChanged(nameof(RemoveUnderwaterFog));
            }
        }

        private bool _removestorm;
        public bool RemoveStorm
        {
            get => _removestorm;
            set
            {
                _removestorm = value;
                OnPropertyChanged(nameof(RemoveStorm));
            }
        }

        private bool _removeclouds;
        public bool RemoveClouds
        {
            get => _removeclouds;
            set
            {
                _removeclouds = value;
                OnPropertyChanged(nameof(RemoveClouds));
            }
        }


        public Color FogColor
        {
            get => _fogColor;
            set
            {
                if (_fogColor != value)
                {
                    _fogColor = value;
                    OnPropertyChanged(nameof(FogColor));
                    UpdateFogColorValues();
                    OnPropertyChanged(nameof(FogBrush));
                }
            }
        }

        public float FogR
        {
            get => _fogR;
            set
            {
                if (_fogR != value)
                {
                    _fogR = value;
                    OnPropertyChanged(nameof(FogR));
                    UpdateFogColorFromRgb();
                }
            }
        }

        public float FogG
        {
            get => _fogG;
            set
            {
                if (_fogG != value)
                {
                    _fogG = value;
                    OnPropertyChanged(nameof(FogG));
                    UpdateFogColorFromRgb();
                }
            }
        }

        public float FogB
        {
            get => _fogB;
            set
            {
                if (_fogB != value)
                {
                    _fogB = value;
                    OnPropertyChanged(nameof(FogB));
                    UpdateFogColorFromRgb();
                }
            }
        }

        public float FogA
        {
            get => _fogA;
            set
            {
                if (_fogA != value)
                {
                    _fogA = value;
                    OnPropertyChanged(nameof(FogA));
                    UpdateFogColorFromRgb();
                }
            }
        }

        private void UpdateFogColorFromRgb()
        {
            _fogColor = Color.FromArgb(
                (byte)(_fogA * 255),
                (byte)(_fogR * 255),
                (byte)(_fogG * 255),
                (byte)(_fogB * 255)
            );
            OnPropertyChanged(nameof(FogColor));
            OnPropertyChanged(nameof(FogBrush));
        }

        private void UpdateFogColorValues()
        {
            FogR = _fogColor.R / 255f;
            FogG = _fogColor.G / 255f;
            FogB = _fogColor.B / 255f;
            FogA = _fogColor.A / 255f;
        }

        public bool EnableFogColor
        {
            get => _enableFogColor;
            set
            {
                if (_enableFogColor != value)
                {
                    _enableFogColor = value;
                    OnPropertyChanged(nameof(EnableFogColor));
                }
            }
        }



        private void UpdateBulletColorFromRgb()
        {
            _bulletColor = Color.FromArgb(
                (byte)(_bulletA * 255),
                (byte)(_bulletR * 255),
                (byte)(_bulletG * 255),
                (byte)(_bulletB * 255)
            );
            OnPropertyChanged(nameof(BulletColor));
        }
        private void UpdateBulletColorValues()
        {
            BulletR = _bulletColor.R / 255f;
            BulletG = _bulletColor.G / 255f;
            BulletB = _bulletColor.B / 255f;
            BulletA = _bulletColor.A / 255f;
        }
        public Color SkyColor
        {
            get => _skyColor;
            set
            {
                if (_skyColor != value)
                {
                    _skyColor = value;
                    OnPropertyChanged(nameof(SkyColor));
                    UpdateSkyColorValues();
                    OnPropertyChanged(nameof(SkyBrush));
                }
            }
        }
        public float SkyR
        {
            get => _skyR;
            set
            {
                if (_skyR != value)
                {
                    _skyR = value;
                    OnPropertyChanged(nameof(SkyR));
                    UpdateSkyColorFromRgb();
                }
            }
        }
        public float SkyG
        {
            get => _skyG;
            set
            {
                if (_skyG != value)
                {
                    _skyG = value;
                    OnPropertyChanged(nameof(SkyG));
                    UpdateSkyColorFromRgb();
                }
            }
        }
        public float SkyB
        {
            get => _skyB;
            set
            {
                if (_skyB != value)
                {
                    _skyB = value;
                    OnPropertyChanged(nameof(SkyB));
                    UpdateSkyColorFromRgb();
                }
            }
        }
        private void UpdateSkyColorFromRgb()
        {
            _skyColor = Color.FromArgb(
                255,
                (byte)(_skyR * 255),
                (byte)(_skyG * 255),
                (byte)(_skyB * 255)
            );
            OnPropertyChanged(nameof(SkyColor));
        }
        private void UpdateSkyColorValues()
        {
            SkyR = _skyColor.R / 255f;
            SkyG = _skyColor.G / 255f;
            SkyB = _skyColor.B / 255f;
        }
        public Color WaterColor
        {
            get => _waterColor;
            set
            {
                if (_waterColor != value)
                {
                    _waterColor = value;
                    OnPropertyChanged(nameof(WaterColor));
                    UpdateWaterColorValues();
                    OnPropertyChanged(nameof(WaterBrush));
                }
            }
        }
        public float WaterR
        {
            get => _waterR;
            set
            {
                if (_waterR != value)
                {
                    _waterR = value;
                    OnPropertyChanged(nameof(WaterR));
                    UpdateWaterColorFromRgb();
                }
            }
        }
        public float WaterG
        {
            get => _waterG;
            set
            {
                if (_waterG != value)
                {
                    _waterG = value;
                    OnPropertyChanged(nameof(WaterG));
                    UpdateWaterColorFromRgb();
                }
            }
        }
        public float WaterB
        {
            get => _waterB;
            set
            {
                if (_waterB != value)
                {
                    _waterB = value;
                    OnPropertyChanged(nameof(WaterB));
                    UpdateWaterColorFromRgb();
                }
            }
        }
        private void UpdateWaterColorFromRgb()
        {
            _waterColor = Color.FromArgb(
                255,
                (byte)(_waterR * 255),
                (byte)(_waterG * 255),
                (byte)(_waterB * 255)
            );
            OnPropertyChanged(nameof(WaterColor));
        }
        private void UpdateWaterColorValues()
        {
            WaterR = _waterColor.R / 255f;
            WaterG = _waterColor.G / 255f;
            WaterB = _waterColor.B / 255f;
        }

        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set
            {
                if (_userInput != value)
                {
                    _userInput = value;
                    OnPropertyChanged(nameof(UserInput));
                }
            }
        }
        public bool IsFovVisible
        {
            get => _isFovVisible;
            set { _isFovVisible = value; OnPropertyChanged(); }
        }
        public bool IsMiscVisible
        {
            get => _isMiscVisible;
            set { _isMiscVisible = value; OnPropertyChanged(); }
        }
        public bool IsWorldVisible
        {
            get => _isWorldVisible;
            set { _isWorldVisible = value; OnPropertyChanged(); }
        }
        public bool IsSettingsVisible
        {
            get => _isSettingsVisible;
            set { _isSettingsVisible = value; OnPropertyChanged(); }
        }
        public bool IsGalleryVisible
        {
            get => _isGalleryVisible;
            set { _isGalleryVisible = value; OnPropertyChanged(); }
        }
        public bool Quickswap
        {
            get => _quickswap;
            set
            {
                _quickswap = value;
                OnPropertyChanged(nameof(Quickswap));
            }
        }
        public bool FastShot
        {
            get => _fastShot;
            set
            {
                _fastShot = value;
                OnPropertyChanged(nameof(FastShot));
            }
        }
        public bool FastReload
        {
            get => _fastReload;
            set
            {
                _fastReload = value;
                OnPropertyChanged(nameof(FastReload));
            }
        }
        public bool InstantZoom
        {
            get => _instantZoom;
            set
            {
                _instantZoom = value;
                OnPropertyChanged(nameof(InstantZoom));
            }
        }
        public bool EnableColorBullets
        {
            get => _enableColorBullets;
            set
            {
                _enableColorBullets = value;
                OnPropertyChanged(nameof(EnableColorBullets));
            }
        }
        public bool EnableFieldOfView
        {
            get => _enableFieldOfView;
            set
            {
                _enableFieldOfView = value;
                OnPropertyChanged(nameof(EnableFieldOfView));

            }
        }
        public bool EnableStaticFov
        {
            get => _enableStaticFov;
            set
            {
                _enableStaticFov = value;
                OnPropertyChanged(nameof(EnableStaticFov));
            }
        }
        public float PlayerFov
        {
            get => _playerFov;
            set { _playerFov = value; OnPropertyChanged(nameof(PlayerFov)); }
        }
        public float SprintFov
        {
            get => _sprintFov;
            set { _sprintFov = value; OnPropertyChanged(nameof(SprintFov)); }
        }
        public float BlunderbussFov
        {
            get => _blunderbussFov;
            set { _blunderbussFov = value; OnPropertyChanged(nameof(BlunderbussFov)); }
        }
        public float EyeOfReachFov
        {
            get => _eyeOfReachFov;
            set { _eyeOfReachFov = value; OnPropertyChanged(nameof(EyeOfReachFov)); }
        }
        public float PistolFov
        {
            get => _pistolFov;
            set { _pistolFov = value; OnPropertyChanged(nameof(PistolFov)); }
        }
        public float CannonFov
        {
            get => _cannonFov;
            set { _cannonFov = value; OnPropertyChanged(nameof(CannonFov)); }
        }
        public float WheelFov
        {
            get => _wheelFov;
            set { _wheelFov = value; OnPropertyChanged(nameof(WheelFov)); }
        }
        public float MapFov
        {
            get => _mapFov;
            set { _mapFov = value; OnPropertyChanged(nameof(MapFov)); }
        }
        public bool RemoveFog
        {
            get => _removeFog;
            set
            {
                _removeFog = value;
                OnPropertyChanged(nameof(RemoveFog));
            }
        }
        public bool EnableSkyColor
        {
            get => _enableSkyColor;
            set
            {
                _enableSkyColor = value;
                OnPropertyChanged(nameof(EnableSkyColor));
            }
        }
        public bool EnableWaterColor
        {
            get => _enableWaterColor;
            set
            {
                _enableWaterColor = value;
                OnPropertyChanged(nameof(EnableWaterColor));
            }
        }

        private WindowManager _windowManager;

        public ICommand SaveCommand { get; }
        public ICommand SavePakToPath { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand GotoFovCommand { get; }
        public ICommand GotoMiscCommand { get; }
        public ICommand GotoWorldCommand { get; }
        public ICommand GotoSettingsCommand { get; }
        public ICommand GotoGalleryCommand { get; }
        public ICommand SaveSettingsToIni { get; }
        public ObservableCollection<ImageWrapper> CurrentImages { get; set; } = new ObservableCollection<ImageWrapper>();
        private readonly ImageLoader _imageLoader;
        public ICommand ShowBlunderCommand { get; }
        public ICommand ShowEorCommand { get; }
        public ICommand ShowPistolCommand { get; }
        public ICommand LoadSettingsIni { get; set; }    

        private string _activeButton;
        public string ActiveButton
        {
            get => _activeButton;
            set
            {
                if (_activeButton != value)
                {
                    _activeButton = value;
                    OnPropertyChanged(nameof(ActiveButton));
                }
            }
        }
        private void SaveSettings()
        {
            SettingsManager.PakPath = UserInput;
            SettingsManager.SaveSettings();
            MessageBox.Show("Path saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public PakManagerVM(ImageLoader imageLoader)
        {
            bool enableFieldOfView, enableStaticFov;
            float playerFov, sprintFov, blunderbussFov, eyeOfReachFov;
            float pistolFov, cannonFov, wheelFov, mapFov;
            bool quickswap, fastShot, fastReload, instantZoom;
            bool enableColorBullets;
            float bulletColorR, bulletColorG, bulletColorB, bulletColorA;
            bool removeFog, enableSkyColor;
            float skyColorR, skyColorG, skyColorB;

            _imageLoader = imageLoader;

            ShowBlunderCommand = new RelayCommand(ShowBlunderImages);
            ShowEorCommand = new RelayCommand(ShowEorImages);
            ShowPistolCommand = new RelayCommand(ShowPistolImages);


            GotoFovCommand = new RelayCommand(() => ShowOnly("Fov"));
            GotoMiscCommand = new RelayCommand(() => ShowOnly("Misc"));
            GotoWorldCommand = new RelayCommand(() => ShowOnly("World"));
            GotoSettingsCommand = new RelayCommand(() => ShowOnly("Settings"));
            GotoGalleryCommand = new RelayCommand(() => ShowOnly("Gallery"));

            CloseWindowCommand = new RelayCommand(() => WindowManager.Instance.CloseWindow());
            MinimizeWindowCommand = new RelayCommand(() => WindowManager.Instance.MinimizeWindow());

            SaveCommand = new RelayCommand(SaveSettings);
            SettingsManager.LoadSettings();
            SavePakToPath = new RelayCommand(() => FinishPak.FinishPakFile(this));
            IniData iniData = new IniData();
            SaveSettingsToIni = new RelayCommand(() => IniData.SaveDataToIni(EnableFieldOfView, EnableStaticFov,
                            PlayerFov, SprintFov, BlunderbussFov, EyeOfReachFov, PistolFov, CannonFov, WheelFov, MapFov,
                            Quickswap, FastShot, FastReload, InstantZoom,
                            EnableColorBullets, BulletR, BulletG, BulletB, BulletA,
                            RemoveFog, EnableSkyColor, SkyR, SkyG, SkyB));
            LoadSettingsIni = new RelayCommand(() => IniData.LoadDataFromIni(out enableFieldOfView, out enableStaticFov,
                            out playerFov, out sprintFov, out blunderbussFov, out eyeOfReachFov,
                            out pistolFov, out cannonFov, out wheelFov, out mapFov,
                            out quickswap, out fastShot, out fastReload, out instantZoom,
                            out enableColorBullets, out bulletColorR, out bulletColorG, out bulletColorB, out bulletColorA,
                            out removeFog, out enableSkyColor, out skyColorR, out skyColorG, out skyColorB));

            /* function to set values from function in cs */


            UserInput = SettingsManager.PakPath;

            PlayerFov = 120;
            SprintFov = 120;
            BlunderbussFov = 120;
            EyeOfReachFov = 120;
            PistolFov = 120;
            CannonFov = 120;
            WheelFov = 120;
            MapFov = 120;
            EnableFieldOfView = true;
            EnableStaticFov = false;
            Quickswap = false;
            FastShot = false;
            FastReload = false;
            InstantZoom = false;
            EnableColorBullets = false;
            RemoveFog = false;
            EnableSkyColor = false;
            BulletR = 1.0f; 
            BulletG = 1.0f; 
            BulletB = 1.0f; 
            BulletA = 1.0f;
            SkyColor = Color.FromArgb(1, 1, 1, 1); //idk why not working but need set alpha for colorpicker.
            WaterColor = Color.FromArgb(1, 1, 1, 1);

            SkyR = 1.0f; // I used it to set real start color.
            SkyG = 1.0f;
            SkyB = 1.0f;

            WaterR = 1.0f;
            WaterG = 1.0f;
            WaterB = 1.0f;

            SunColor = Color.FromArgb(1, 0, 1, 1);
            MoonColor = Color.FromArgb(1, 1, 0, 1);
            FogColor = Color.FromArgb(0, 1, 0, 1);

            SunR = 1.0f;
            SunG = 1.0f;
            SunB = 1.0f;
            SunA = 1.0f;

            MoonR = 1.0f;
            MoonG = 1.0f;
            MoonB = 1.0f;
            MoonA = 1.0f;

            FogR = 1.0f;
            FogG = 1.0f;
            FogB = 1.0f;
            FogA = 1.0f;

            ShowOnly("Fov");
            ShowBlunderImages();
        }
        private void ShowOnly(string section)
        {
            IsFovVisible = false;
            IsMiscVisible = false;
            IsWorldVisible = false;
            IsSettingsVisible = false;
            IsGalleryVisible = false;

            switch (section)
            {
                case "Fov":
                    IsFovVisible = true;
                    break;
                case "Misc":
                    IsMiscVisible = true;
                    break;
                case "World":
                    IsWorldVisible = true;
                    break;
                case "Settings":
                    IsSettingsVisible = true;
                    break;
                case "Gallery":
                    IsGalleryVisible = true;
                    break;
            }
        }
        private void ShowBlunderImages()
        {
            ActiveButton = "Blunder";
            UpdateCurrentImages(_imageLoader.BlunderImages, _imageLoader.BlunderPacks.ToList());
        }
        private void ShowEorImages()
        {
            ActiveButton = "Eor";
            UpdateCurrentImages(_imageLoader.EorImages, _imageLoader.EorPacks.ToList());
        }
        private void ShowPistolImages()
        {
            ActiveButton = "Pistol";
            UpdateCurrentImages(_imageLoader.PistolImages, _imageLoader.PistolPacks.ToList());
        }

        private void UpdateCurrentImages(ObservableCollection<BitmapImage> newImages, List<string> packs)
        {
            CurrentImages.Clear();
            for (int i = 0; i < newImages.Count; i++)
            {
                string packName = (i < packs.Count) ? packs[i] : "Unknown";
                CurrentImages.Add(new ImageWrapper(newImages[i], false, packName));
            }
        }
        public void HighlightImage(ImageWrapper image)
        {
            foreach (var img in CurrentImages)
            {
                img.IsHighlighted = false;
            }

            image.IsHighlighted = true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
