using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WMPLib;
using NAudio;
using NAudio.Wave;

namespace Madu
{
    class Sounds
    {
        private string pathToMedia;
        private IWavePlayer outputDevice;
        private AudioFileReader audioFileReader;

        public Sounds(string pathToResources)
        {
            pathToMedia = pathToResources;
        }

        public void Play()
        {
            PlayFile("gameover.mp3", volume: 0.3f, loop: true);
        }

        public void Play(string songName)
        {
            PlayFile(songName, volume: 1.0f, loop: false);
        }

        public void PlayEat()
        {
            PlayFile("eat.mp3", volume: 1.0f, loop: false);
        }

        private void PlayFile(string fileName, float volume = 1.0f, bool loop = false)
        {
            Stop(); 

            string fullPath = Path.Combine(pathToMedia, fileName);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Файл не найден: {fullPath}");

            audioFileReader = new AudioFileReader(fullPath)
            {
                Volume = volume
            };

            if (loop)
            {
                var loopStream = new LoopStream(audioFileReader);
                outputDevice = new WaveOutEvent();
                //outputDevice.Init(loopStream);
            }
            else
            {
                outputDevice = new WaveOutEvent();
                //outputDevice.Init(audioFileReader);
            }

            //outputDevice.Play();
        }

        public void Stop()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            outputDevice = null;

            audioFileReader?.Dispose();
            audioFileReader = null;
        }

        public void Dispose()
        {
            Stop();
        }
        //WindowsMediaPlayer player = new WindowsMediaPlayer();
        //private string pathToMedia;

        //public Sounds(string pathToResources)
        //{
        //    pathToMedia = pathToResources;
        //}

        //public void Play()
        //{
        //    player.URL = pathToMedia + @"gameover.mp3";
        //    player.settings.volume = 30;
        //    player.controls.play();
        //    player.settings.setMode("loop", true);
        //}

        //public void Play(string songName)
        //{
        //    player.URL = pathToMedia + songName;
        //    player.controls.play();
        //}

        //public void PlayEat()
        //{
        //    player.URL = pathToMedia + @"eat.mp3";
        //    player.settings.volume = 100;
        //    player.controls.play();
        //}
    }
}
